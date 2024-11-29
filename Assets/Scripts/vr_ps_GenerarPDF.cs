using iTextSharp.text.pdf.draw;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using sysio = System.IO;
using iFont = iTextSharp.text;
using UnityEngine;
using TMPro;
using System.Linq;

public class vr_ps_GenerarPDF : MonoBehaviour
{
    public static vr_ps_GenerarPDF Instance;

    void Awake()
    {
        Instance = this;
    }

    /*public TMP_InputField inputNombres;
    public TMP_InputField inputApellidos;
    public TMP_InputField inputCedula;
    public TMP_InputField inputCorreo;
    public TMP_InputField inputCelular;*/

    [SerializeField] private GameObject msgError;
    [SerializeField] private TMP_Text aciertosvrps01;
    [SerializeField] private TMP_Text erroresvrps01;
    [SerializeField] private TMP_Text timevrps01;
    [SerializeField] private TMP_Text aciertosvrps02;
    [SerializeField] private TMP_Text erroresvrps02;
    [SerializeField] private TMP_Text timevrps02;
    [SerializeField] private TMP_Text erroresvrps03;
    [SerializeField] private TMP_Text timevrps03;
    //[SerializeField] private TMP_Text msgErrorPDF;

    private int porcentajePeriferica;
    private int porcentajeReaccion;
    private int maxErroresPrecision;
    private float maxTimePrecision;

    private string cedula;
    private string nombres;
    private string apellidos;
    private string correo;
    private string celular;

    private string directory = sysio.Directory.GetCurrentDirectory();
    private string nombreArchivo = " ";

    // Start is called before the first frame update
    void Start()
    {
        leerConfig();
        leerDataUser();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && msgError.activeInHierarchy)
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }

    public void GenerarPDF()
    {
        if (CrearPDF())
        {
            System.Diagnostics.Process.Start("explorer.exe", sysio.Path.Combine(directory, "GeneratorPDF", nombreArchivo));
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
        else
        {
            Debug.Log("Error al generar el pdf");
            msgError.SetActive(true);
            aciertosvrps01.text = "<b>Aciertos<b>\n" + vr_ps_singleton.Instance.GetPerifericaAcierto().ToString();
            erroresvrps01.text = "<b>Errores<b>\n" + vr_ps_singleton.Instance.GetPerifericaErrores().ToString();
            timevrps01.text = "<b>Timepo<b>\n" + vr_ps_singleton.Instance.GetPerifericaTime();
            aciertosvrps02.text = "<b>Aciertos<b>\n" + vr_ps_singleton.Instance.GetReaccionAcierto().ToString();
            erroresvrps02.text = "<b>Errores<b>\n" + vr_ps_singleton.Instance.GetReaccionErrores().ToString();
            timevrps02.text = "<b>Tiempo<b>\n" + vr_ps_singleton.Instance.GetReaccionTime();
            erroresvrps03.text = "<b>Errores<b>\n" + vr_ps_singleton.Instance.GetPrecisionErrores().ToString();
            timevrps03.text = "<b>Tiempo<b>\n" + vr_ps_singleton.Instance.GetPrecisionTime();
        }
    }

    private void GeneradorNombreArchivo()
    {
        int count = 1;
        while (sysio.File.Exists(sysio.Path.Combine(directory, "GeneratorPDF", nombreArchivo + "-result" + count.ToString() + ".pdf")))
        {
            count++;
        }
        nombreArchivo = nombreArchivo + "-result" + count.ToString() + ".pdf";
    }

    private bool CrearPDF()
    {
        if (!sysio.File.Exists(sysio.Path.Combine(directory, "dataUser.temp")))
        {
            return false;
        }
        else
        {
            sysio.File.Delete(sysio.Path.Combine(directory, "dataUser.temp"));
        }

        int aciertosPrueba1 = vr_ps_singleton.Instance.GetPerifericaAcierto();
        int erroresPrueba1 = vr_ps_singleton.Instance.GetPerifericaErrores();
        string tiempoPrueba1 = vr_ps_singleton.Instance.GetPerifericaTime();

        int aciertosPrueba2 = vr_ps_singleton.Instance.GetReaccionAcierto();
        int erroresPrueba2 = vr_ps_singleton.Instance.GetReaccionErrores();
        string tiempoPrueba2 = vr_ps_singleton.Instance.GetReaccionTime();

        string completadoPrueba3 = "Si";
        int erroresPrueba3 = vr_ps_singleton.Instance.GetPrecisionErrores();
        string tiempoPrueba3 = vr_ps_singleton.Instance.GetPrecisionTime();

        /*int aciertosPrueba1 = 32;
        int erroresPrueba1 = 8;
        string tiempoPrueba1 = "3:15";

        int aciertosPrueba2 = 17;
        int erroresPrueba2 = 23;
        string tiempoPrueba2 = "2:55";

        string completadoPrueba3 = "Si";
        int erroresPrueba3 = 5;
        string tiempoPrueba3 = "1:52";*/

        string dirLogoUniversidad = sysio.Path.Combine(directory, "Assets", "Logo_universidad_UTM.png");
        string dirLogoFacultad = sysio.Path.Combine(directory, "Assets", "LogoFCI-BadQuality.png");

        DateTime fecha = DateTime.Now;
        string dia = fecha.ToString("dd");
        string mes = fecha.ToString("MMMM");
        string año = fecha.ToString("yyyy");

        Document doc = new Document();

        try
        {
            //Source
            Paragraph paragraphEspacio = new Paragraph("\n", new iFont.Font(null, 5));
            Chunk lineaSeparadora = new Chunk(new LineSeparator(0.5f, 100, BaseColor.BLACK, Element.ALIGN_CENTER, 1));

            //Fecha
            iFont.Font fontFecha = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 10, iFont.Font.NORMAL);
            Paragraph paragraphFecha = new Paragraph("Portoviejo, " + dia + " de " + mes + " del " + año, fontFecha);
            paragraphFecha.Alignment = Element.ALIGN_RIGHT;

            //Logo Universidad
            Image logoUniversidad = Image.GetInstance(dirLogoUniversidad);
            logoUniversidad.SetAbsolutePosition(25f, 700f);
            logoUniversidad.ScaleAbsolute(75f, 75f);

            //Titulo
            iFont.Font fontTitulo = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 20, iFont.Font.BOLD);
            Paragraph paragraphTitulo = new Paragraph("REALIDAD VIRTUAL PARA PRUEBAS PSICOSENSOMÉTRICAS", fontTitulo);
            paragraphTitulo.Alignment = Element.ALIGN_CENTER;

            //Logo Universidad
            Image logoFacultad = Image.GetInstance(dirLogoFacultad);
            logoFacultad.SetAbsolutePosition(490f, 700f);
            logoFacultad.ScaleAbsolute(75f, 75f);

            //SubTitulo
            iFont.Font fontSubTitulo = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 14, iFont.Font.BOLD | iFont.Font.UNDERLINE);
            Paragraph paragraphSubTitulo = new Paragraph("RESULTADOS DE LA EVALUACIÓN", fontSubTitulo);
            paragraphSubTitulo.Alignment = Element.ALIGN_CENTER;

            //Datos Evaluado
            iFont.Font fontDatosEvaluado = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.BOLD);
            Paragraph paragraphDatosEvaluado = new Paragraph("DATOS DEL EVALUADO", fontDatosEvaluado);
            paragraphDatosEvaluado.Alignment = Element.ALIGN_CENTER;

            //Cedula Evaluado
            iFont.Font fontCedulaEvaluado = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.BOLD);
            iFont.Font fontDATACedulaEvaluado = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.NORMAL);
            Paragraph paragraphCedulaEvaluado = new Paragraph("Cédula: ", fontCedulaEvaluado);
            Chunk chunkDATACedulaEvaluado = new Chunk(cedula, fontDATACedulaEvaluado);
            paragraphCedulaEvaluado.AddSpecial(chunkDATACedulaEvaluado);
            paragraphCedulaEvaluado.Alignment = Element.ALIGN_LEFT;

            //Nombre Evaluado
            iFont.Font fontNombreEvaluado = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.BOLD);
            iFont.Font fontDATANombreEvaluado = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.NORMAL);
            Paragraph paragraphNombreEvaluado = new Paragraph("Nombres: ", fontNombreEvaluado);
            Chunk chunkDATANombreEvaluado = new Chunk(nombres + " " + apellidos, fontDATANombreEvaluado);
            paragraphNombreEvaluado.AddSpecial(chunkDATANombreEvaluado);
            paragraphNombreEvaluado.Alignment = Element.ALIGN_LEFT;

            //Correo Evaluado
            iFont.Font fontCorreoEvaluado = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.BOLD);
            iFont.Font fontDATACorreoEvaluado = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.NORMAL);
            Paragraph paragraphCorreoEvaluado = new Paragraph("Correo: ", fontCorreoEvaluado);
            Chunk chunkDATACorreoEvaluado = new Chunk(correo, fontDATACorreoEvaluado);
            paragraphCorreoEvaluado.AddSpecial(chunkDATACorreoEvaluado);
            paragraphCorreoEvaluado.Alignment = Element.ALIGN_LEFT;

            //Correo Evaluado
            iFont.Font fontCelularEvaluado = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.BOLD);
            iFont.Font fontDATACelularEvaluado = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.NORMAL);
            Paragraph paragraphCelularEvaluado = new Paragraph("Celular: ", fontCelularEvaluado);
            Chunk chunkDATACelularEvaluado = new Chunk(celular, fontDATACelularEvaluado);
            paragraphCelularEvaluado.AddSpecial(chunkDATACelularEvaluado);
            paragraphCelularEvaluado.Alignment = Element.ALIGN_LEFT;

            //Resultados
            iFont.Font fontResultados = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.BOLD);
            Paragraph paragraphResultados = new Paragraph("RESULTADOS", fontResultados);
            paragraphResultados.Alignment = Element.ALIGN_CENTER;

            //Prueba 1
            iFont.Font fontPrueba1 = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.BOLD | iFont.Font.UNDERLINE);
            Paragraph paragraphPrueba1 = new Paragraph("Prueba de Visión Periférica", fontPrueba1);
            paragraphPrueba1.Alignment = Element.ALIGN_LEFT;

            //Aciertos Prueba 1
            iFont.Font fontAciertosPrueba1 = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.BOLD);
            iFont.Font fontDATAAciertosPrueba1 = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.NORMAL);
            Paragraph paragraphAciertosPrueba1 = new Paragraph("      > Aciertos: ", fontAciertosPrueba1);
            Chunk chunkDATAAciertosPrueba1 = new Chunk(aciertosPrueba1.ToString(), fontDATAAciertosPrueba1);
            paragraphAciertosPrueba1.AddSpecial(chunkDATAAciertosPrueba1);
            paragraphAciertosPrueba1.Alignment = Element.ALIGN_LEFT;

            //Errores Prueba 1
            iFont.Font fontErroresPrueba1 = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.BOLD);
            iFont.Font fontDATAErroresPrueba1 = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.NORMAL);
            Paragraph paragraphErroresPrueba1 = new Paragraph("      > Errores: ", fontErroresPrueba1);
            Chunk chunkDATAErroresPrueba1 = new Chunk(erroresPrueba1.ToString(), fontDATAErroresPrueba1);
            paragraphErroresPrueba1.AddSpecial(chunkDATAErroresPrueba1);
            paragraphErroresPrueba1.Alignment = Element.ALIGN_LEFT;

            //Tiempo Prueba 1
            iFont.Font fontTiempoPrueba1 = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.BOLD);
            iFont.Font fontDATATiempoPrueba1 = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.NORMAL);
            Paragraph paragraphTiempoPrueba1 = new Paragraph("      > Tiempo: ", fontTiempoPrueba1);
            Chunk chunkDATATiempoPrueba1 = new Chunk(tiempoPrueba1, fontDATATiempoPrueba1);
            paragraphTiempoPrueba1.AddSpecial(chunkDATATiempoPrueba1);
            paragraphTiempoPrueba1.Alignment = Element.ALIGN_LEFT;

            //Observacion Prueba 1
            iFont.Font fontObservacionPrueba1 = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.BOLD);
            iFont.Font fontDATAObservacionPrueba1 = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.NORMAL);
            Paragraph paragraphObservacionPrueba1 = new Paragraph("      > Observación: ", fontObservacionPrueba1);
            Chunk chunkDATAObservacionPrueba1 = new Chunk(CalcularAprovacionPeriferica(aciertosPrueba1, erroresPrueba1), fontDATAObservacionPrueba1);
            paragraphObservacionPrueba1.AddSpecial(chunkDATAObservacionPrueba1);
            paragraphObservacionPrueba1.Alignment = Element.ALIGN_LEFT;

            //Prueba 2
            iFont.Font fontPrueba2 = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.BOLD | iFont.Font.UNDERLINE);
            Paragraph paragraphPrueba2 = new Paragraph("Prueba de Reacción", fontPrueba2);
            paragraphPrueba2.Alignment = Element.ALIGN_LEFT;

            //Aciertos Prueba 2
            iFont.Font fontAciertosPrueba2 = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.BOLD);
            iFont.Font fontDATAAciertosPrueba2 = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.NORMAL);
            Paragraph paragraphAciertosPrueba2 = new Paragraph("      > Aciertos: ", fontAciertosPrueba2);
            Chunk chunkDATAAciertosPrueba2 = new Chunk(aciertosPrueba2.ToString(), fontDATAAciertosPrueba2);
            paragraphAciertosPrueba2.AddSpecial(chunkDATAAciertosPrueba2);
            paragraphAciertosPrueba2.Alignment = Element.ALIGN_LEFT;

            //Errores Prueba 2
            iFont.Font fontErroresPrueba2 = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.BOLD);
            iFont.Font fontDATAErroresPrueba2 = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.NORMAL);
            Paragraph paragraphErroresPrueba2 = new Paragraph("      > Errores: ", fontErroresPrueba2);
            Chunk chunkDATAErroresPrueba2 = new Chunk(erroresPrueba2.ToString(), fontDATAErroresPrueba2);
            paragraphErroresPrueba2.AddSpecial(chunkDATAErroresPrueba2);
            paragraphErroresPrueba2.Alignment = Element.ALIGN_LEFT;

            //Tiempo Prueba 2
            iFont.Font fontTiempoPrueba2 = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.BOLD);
            iFont.Font fontDATATiempoPrueba2 = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.NORMAL);
            Paragraph paragraphTiempoPrueba2 = new Paragraph("      > Tiempo: ", fontTiempoPrueba2);
            Chunk chunkDATATiempoPrueba2 = new Chunk(tiempoPrueba2, fontDATATiempoPrueba2);
            paragraphTiempoPrueba2.AddSpecial(chunkDATATiempoPrueba2);
            paragraphTiempoPrueba2.Alignment = Element.ALIGN_LEFT;

            //Observacion Prueba 2
            iFont.Font fontObservacionPrueba2 = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.BOLD);
            iFont.Font fontDATAObservacionPrueba2 = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.NORMAL);
            Paragraph paragraphObservacionPrueba2 = new Paragraph("      > Observación: ", fontObservacionPrueba2);
            Chunk chunkDATAObservacionPrueba2 = new Chunk(CalcularAprovacionReaccion(aciertosPrueba2, erroresPrueba2), fontDATAObservacionPrueba2);
            paragraphObservacionPrueba2.AddSpecial(chunkDATAObservacionPrueba2);
            paragraphObservacionPrueba2.Alignment = Element.ALIGN_LEFT;

            //Prueba 3
            iFont.Font fontPrueba3 = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.BOLD | iFont.Font.UNDERLINE);
            Paragraph paragraphPrueba3 = new Paragraph("Prueba de Precisión", fontPrueba3);
            paragraphPrueba2.Alignment = Element.ALIGN_LEFT;

            //Aciertos Prueba 3
            iFont.Font fontCompletadoPrueba3 = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.BOLD);
            iFont.Font fontDATACompletadoPrueba3 = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.NORMAL);
            Paragraph paragraphCompletadoPrueba3 = new Paragraph("      > Pista Completada: ", fontCompletadoPrueba3);
            Chunk chunkDATACompletadoPrueba3 = new Chunk(completadoPrueba3.ToString(), fontDATACompletadoPrueba3);
            paragraphCompletadoPrueba3.AddSpecial(chunkDATACompletadoPrueba3);
            paragraphCompletadoPrueba3.Alignment = Element.ALIGN_LEFT;

            //Errores Prueba 3
            iFont.Font fontErroresPrueba3 = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.BOLD);
            iFont.Font fontDATAErroresPrueba3 = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.NORMAL);
            Paragraph paragraphErroresPrueba3 = new Paragraph("      > Errores: ", fontErroresPrueba3);
            Chunk chunkDATAErroresPrueba3 = new Chunk(erroresPrueba3.ToString(), fontDATAErroresPrueba3);
            paragraphErroresPrueba3.AddSpecial(chunkDATAErroresPrueba3);
            paragraphErroresPrueba3.Alignment = Element.ALIGN_LEFT;

            //Tiempo Prueba 3
            iFont.Font fontTiempoPrueba3 = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.BOLD);
            iFont.Font fontDATATiempoPrueba3 = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.NORMAL);
            Paragraph paragraphTiempoPrueba3 = new Paragraph("      > Tiempo: ", fontTiempoPrueba3);
            Chunk chunkDATATiempoPrueba3 = new Chunk(tiempoPrueba3, fontDATATiempoPrueba3);
            paragraphTiempoPrueba3.AddSpecial(chunkDATATiempoPrueba3);
            paragraphTiempoPrueba3.Alignment = Element.ALIGN_LEFT;

            //Observacion Prueba 2
            iFont.Font fontObservacionPrueba3 = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.BOLD);
            iFont.Font fontDATAObservacionPrueba3 = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.NORMAL);
            Paragraph paragraphObservacionPrueba3 = new Paragraph("      > Observación: ", fontObservacionPrueba2);
            Chunk chunkDATAObservacionPrueba3 = new Chunk(AprovacionPrecision(erroresPrueba3, tiempoPrueba3), fontDATAObservacionPrueba3);
            paragraphObservacionPrueba3.AddSpecial(chunkDATAObservacionPrueba3);
            paragraphObservacionPrueba3.Alignment = Element.ALIGN_LEFT;

            //Nota
            iFont.Font fontNota = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 8, iFont.Font.BOLD);
            iFont.Font fontDATANota = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 8, iFont.Font.NORMAL);
            Paragraph paragraphNota = new Paragraph("Nota: ", fontNota);
            Chunk chunkDATANota = new Chunk("Los resultados presentados en este documento son generados con el propósito de demostrar el funcionamiento del " +
                "prototipo de REALIDAD VIRTUAL PARA PRUEBAS PSICOSENSOMÉTRICAS. No poseen validez oficial ni representan a las evaluaciones reales. " +
                "Este documento NO debe considerarse como resultados de una evaluación legtima o certificación psicosensométricas.", fontDATANota);
            paragraphNota.AddSpecial(chunkDATANota);
            paragraphNota.Alignment = Element.ALIGN_CENTER;

            float anchoBarra = 30f;

            //TABLA PRUEBA 1
            int[] datosprueba1 = { aciertosPrueba1, erroresPrueba1 }; // Ajusta los valores según tus datos
            float prueba1maxValue = datosprueba1.Max(); // Obtener el valor máximo de los datos
            string[] prueba1labels = { "Aciertos: " + aciertosPrueba1, "Errrores: " + erroresPrueba1 };

            // Calcular la altura máxima de la barra
            float prueba1maxHeight = 70f; // Altura máxima de la barra en puntos

            // Calcular la altura de cada barra proporcionalmente a su valor
            float[] prueba1barHeights = datosprueba1.Select(value => (value / prueba1maxValue) * prueba1maxHeight).ToArray();

            //Titulo Tabla Prueba 1
            iFont.Font fontTablaPrueba1 = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.BOLD);
            Paragraph paragraphTablaprueba1 = new Paragraph("Tabla de la Prueba 1", fontTablaPrueba1);
            paragraphTablaprueba1.Alignment = Element.ALIGN_CENTER;

            PdfPTable tableprueba1 = new PdfPTable(datosprueba1.Length);
            tableprueba1.HorizontalAlignment = Element.ALIGN_LEFT;
            tableprueba1.WidthPercentage = 30; // Ajusta el ancho de la tabla al 100% del ancho de la página

            // Configurar el ancho de la columna para las barras
            float[] columnWidths1 = new float[datosprueba1.Length];
            for (int i = 0; i < datosprueba1.Length; i++)
            {
                columnWidths1[i] = anchoBarra; // Configura el ancho de cada columna
            }
            tableprueba1.SetTotalWidth(columnWidths1);

            // Agregar celdas a la tabla para representar las barras del gráfico
            for (int i = 0; i < datosprueba1.Length; i++)
            {
                PdfPCell cell = new PdfPCell();
                cell.FixedHeight = 100f; // Altura fija de la celda para la barra y el nombre
                cell.Border = 0;
                cell.HorizontalAlignment = Element.ALIGN_LEFT; // Alinear a la izquierda
                cell.VerticalAlignment = Element.ALIGN_BOTTOM; // Alinear hacia abajo

                // Dibujar un rectángulo coloreado para representar la barra
                PdfPTable barTable = new PdfPTable(1);
                PdfPCell barCell = new PdfPCell();
                barCell.FixedHeight = prueba1barHeights[i]; // Utiliza la altura calculada para esta barra
                barCell.Border = 0;
                barCell.BackgroundColor = BaseColor.BLUE; // Cambia el color de la barra según tu preferencia
                barTable.AddCell(barCell);

                cell.AddElement(barTable);

                // Agregar un texto con la etiqueta encima de cada barra
                Paragraph labelParagraph = new Paragraph(prueba1labels[i]);
                labelParagraph.Alignment = Element.ALIGN_CENTER;
                labelParagraph.Font.Size = 10; // Tamaño de fuente ajustado para que quepa en la celda
                cell.AddElement(labelParagraph);

                tableprueba1.AddCell(cell);
            }

            // TABLA PRUEBA 2
            int[] datosprueba2 = { aciertosPrueba2, erroresPrueba2 }; // Ajusta los valores según tus datos
            string[] prueba2labels = { "Acieros: " + aciertosPrueba2, "Errores: " + erroresPrueba2 };

            // Calcular la altura máxima de la barra
            float prueba2maxHeight = 70f; // Altura máxima de la barra en puntos
            float prueba2maxValue = datosprueba2.Max(); // Obtener el valor máximo de los datos

            // Calcular la altura de cada barra proporcionalmente a su valor
            float[] prueba2barHeights = datosprueba2.Select(value => (value / prueba2maxValue) * prueba2maxHeight).ToArray();

            //Titulo Tabla Prueba 2
            iFont.Font fontTablaprueba2 = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.BOLD);
            Paragraph paragraphTablaprueba2 = new Paragraph("Tabla de la Prueba 2", fontTablaprueba2);
            paragraphTablaprueba2.Alignment = Element.ALIGN_CENTER;

            PdfPTable tableprueba2 = new PdfPTable(datosprueba2.Length);
            tableprueba2.HorizontalAlignment = Element.ALIGN_LEFT;

            tableprueba2.WidthPercentage = 30; // Ajusta el ancho de la tabla al 100% del ancho de la página

            // Configurar el ancho de la columna para las barras
            float[] columnWidths2 = new float[datosprueba2.Length];
            for (int i = 0; i < datosprueba1.Length; i++)
            {
                columnWidths2[i] = anchoBarra; // Configura el ancho de cada columna
            }
            tableprueba2.SetTotalWidth(columnWidths2);
            // Agregar celdas a la tabla para representar las barras del gráfico
            for (int i = 0; i < datosprueba2.Length; i++)
            {
                PdfPCell cell = new PdfPCell();
                cell.FixedHeight = 100f; // Altura fija de la celda para la barra y el nombre
                cell.Border = 0;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_BOTTOM;

                // Dibujar un rectángulo coloreado para representar la barra
                PdfPTable barTable = new PdfPTable(1);
                PdfPCell barCell = new PdfPCell();
                barCell.FixedHeight = prueba2barHeights[i]; // Utiliza la altura calculada para esta barra
                barCell.Border = 0;
                barCell.BackgroundColor = BaseColor.LIGHT_GRAY; // Cambia el color de la barra según tu preferencia
                barTable.AddCell(barCell);

                cell.AddElement(barTable);

                // Agregar un texto con la etiqueta encima de cada barra
                Paragraph labelParagraph = new Paragraph(prueba2labels[i]);
                labelParagraph.Alignment = Element.ALIGN_CENTER;
                labelParagraph.Font.Size = 10; // Tamaño de fuente ajustado para que quepa en la celda
                cell.AddElement(labelParagraph);

                tableprueba2.AddCell(cell);
            }

            // TABLA PRUEBA 3
            string auxTime = tiempoPrueba3.Replace(":", ",");
            float timeFloat = float.Parse(auxTime);
            float[] datosprueba3 = { erroresPrueba3, timeFloat }; // Ajusta los tiempos según tus datos
            string[] prueba3Labels = { "Errores: " + erroresPrueba3, "Tiempo: " + tiempoPrueba3 };

            // Calcular la altura máxima de la barra (por ejemplo, en puntos por segundo)
            float prueba3maxHeight = 70f; // Ajusta según tus necesidades de diseño
            float prueba3maxValue = datosprueba3.Max();

            /* Calcular la altura de cada barra proporcionalmente a la duración de tiempo que representan
            float[] tiemposBarHeights = tiemposData.Select(str =>
                (float)(TimeSpan.Parse(str).TotalSeconds / maxTimeSpan.TotalSeconds) * maxHeightPerSecond).ToArray();*/

            // Calcular la altura de cada barra proporcionalmente a su valor
            float[] prueba3barHeights = datosprueba3.Select(value => (value / prueba3maxValue) * prueba3maxHeight).ToArray();

            //Titulo Tabla Prueba 3
            iFont.Font fontTablaPrueba3 = new iFont.Font(iFont.Font.FontFamily.HELVETICA, 11, iFont.Font.BOLD);
            Paragraph paragraphTablaPrueba3 = new Paragraph("Tabla de la Prueba 3", fontTablaPrueba3);
            paragraphTablaPrueba3.Alignment = Element.ALIGN_CENTER;

            PdfPTable tableprueba3 = new PdfPTable(datosprueba3.Length);
            tableprueba3.HorizontalAlignment = Element.ALIGN_LEFT;

            tableprueba3.WidthPercentage = 30; // Ajusta el ancho de la tabla al 100% del ancho de la página

            // Configurar el ancho de la columna para las barras
            float[] columnWidths3 = new float[datosprueba3.Length];
            for (int i = 0; i < datosprueba1.Length; i++)
            {
                columnWidths3[i] = anchoBarra; // Configura el ancho de cada columna
            }
            tableprueba3.SetTotalWidth(columnWidths3);
            // Agregar celdas a la tabla para representar las barras del gráfico
            for (int i = 0; i < datosprueba3.Length; i++)
            {
                PdfPCell cell = new PdfPCell();
                cell.FixedHeight = 100f; // Altura fija de la celda para la barra y el nombre
                cell.Border = 0;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_BOTTOM;

                // Dibujar un rectángulo coloreado para representar la barra
                PdfPTable barTable = new PdfPTable(1);
                PdfPCell barCell = new PdfPCell();
                barCell.FixedHeight = prueba3barHeights[i]; // Utiliza la altura calculada para esta barra
                barCell.Border = 0;
                barCell.BackgroundColor = BaseColor.YELLOW; // Cambia el color de la barra según tu preferencia
                barTable.AddCell(barCell);

                cell.AddElement(barTable);

                // Agregar un texto con la etiqueta encima de cada barra
                Paragraph labelParagraph = new Paragraph(prueba3Labels[i]);
                labelParagraph.Alignment = Element.ALIGN_CENTER;
                labelParagraph.Font.Size = 10; // Tamaño de fuente ajustado para que quepa en la celda
                cell.AddElement(labelParagraph);

                tableprueba3.AddCell(cell);
            }

            string primerasNombre = nombres.Substring(0, Math.Min(3, nombres.Length));
            string primerasApellido = apellidos.Substring(0, Math.Min(4, apellidos.Length));
            int longitud = Math.Min(4, cedula.Length);
            string ultimasCedula = cedula.Substring(cedula.Length - longitud, longitud);
            nombreArchivo = "vrps-" + primerasNombre.ToLower() + primerasApellido.ToLower() + ultimasCedula;
            GeneradorNombreArchivo();

            PdfWriter.GetInstance(doc, new sysio.FileStream(sysio.Path.Combine("GeneratorPDF", nombreArchivo), sysio.FileMode.Create));

            doc.Open();

            //Encabezado
            doc.Add(paragraphFecha);
            doc.Add(logoUniversidad);
            doc.Add(paragraphTitulo);
            doc.Add(logoFacultad);
            doc.Add(paragraphSubTitulo);

            doc.Add(paragraphEspacio);
            doc.Add(lineaSeparadora);

            //Datos del Evaluado
            doc.Add(paragraphDatosEvaluado);
            doc.Add(paragraphCedulaEvaluado);
            doc.Add(paragraphNombreEvaluado);
            doc.Add(paragraphCorreoEvaluado);
            doc.Add(paragraphCelularEvaluado);

            doc.Add(lineaSeparadora);

            //Resultados
            doc.Add(paragraphResultados);
            doc.Add(paragraphEspacio);
            //Prueba1
            doc.Add(paragraphPrueba1);
            /*doc.Add(paragraphAciertosPrueba1);
            doc.Add(paragraphErroresPrueba1);*/
            doc.Add(tableprueba1);
            doc.Add(paragraphTiempoPrueba1);
            doc.Add(paragraphObservacionPrueba1);
            doc.Add(paragraphEspacio);
            //Prueba2
            doc.Add(paragraphPrueba2);
            /*doc.Add(paragraphAciertosPrueba2);
            doc.Add(paragraphErroresPrueba2);*/
            doc.Add(tableprueba2);
            doc.Add(paragraphTiempoPrueba2);
            doc.Add(paragraphObservacionPrueba2);
            doc.Add(paragraphEspacio);
            //Prueba3
            doc.Add(paragraphPrueba3);
            /*doc.Add(paragraphCompletadoPrueba3);
            doc.Add(paragraphErroresPrueba3);
            doc.Add(paragraphTiempoPrueba3);*/
            doc.Add(tableprueba3);
            doc.Add(paragraphObservacionPrueba3);
            /*Nota
            doc.Add(lineaSeparadora);
            doc.Add(paragraphNota);*/
            /*Tablas
            doc.Add(paragraphTablaAciertos);
            doc.Add(tableAciertos);
            doc.Add(paragraphEspacio);
            doc.Add(paragraphEspacio);
            doc.Add(paragraphTablaErrores);
            doc.Add(tableErrores);
            doc.Add(paragraphEspacio);
            doc.Add(paragraphEspacio);
            doc.Add(paragraphTablaTime);
            doc.Add(tableTime);
            doc.Add(paragraphEspacio);*/


            doc.Close();

            //Process.Start("explorer.exe", Path.Combine(directory, "archivo.pdf"));
            return true;
        }
        catch (Exception err)
        {
            Debug.Log("Error al generar el pdf -- MsgErr: " + err);
            //msgErrorPDF.text = "Error al generar el pdf -- MsgErr: " + err;
            return false;
        }
    }

    private string CalcularAprovacionPeriferica(int aciertos, int errores)
    {
        int n = aciertos + errores;
        float prom = (float)aciertos / (float)n;
        if((prom * 100) >= porcentajePeriferica)
        {
            return "Aprobado";
        }
        else
        {
            return "Desaprobado";
        }
    }

    private string CalcularAprovacionReaccion(int aciertos, int errores)
    {
        int n = aciertos + errores;
        float prom = (float)aciertos / (float)n;
        if ((prom * 100) >= porcentajeReaccion)
        {
            return "Aprobado";
        }
        else
        {
            return "Desaprobado";
        }
    }

    private string AprovacionPrecision(int errores, string time)
    {
        string auxTime = time.Replace(":", ",");
        float timeFloat = float.Parse(auxTime);
        if (errores <= maxErroresPrecision && timeFloat <= maxTimePrecision)
        {
            return "Aprobado";
        }
        else
        {
            return "Desaprobado";
        }
    }

    private string ExtraerData(string linea)
    {
        int indexSeparator = linea.IndexOf(':');
        string auxLinea = linea.Substring(indexSeparator + 1);
        return auxLinea;
    }

    private void leerConfig()
    {
        using (sysio.StreamReader reader = new sysio.StreamReader(".conf"))
        {
            string linea;
            while ((linea = reader.ReadLine()) != null)
            {
                if (linea.Contains("pvpAciertos"))
                {
                    porcentajePeriferica = int.Parse(ExtraerData(linea));
                }
                if (linea.Contains("prAciertos"))
                {
                    porcentajeReaccion = int.Parse(ExtraerData(linea));
                }
                if (linea.Contains("ppErrores"))
                {
                    maxErroresPrecision = int.Parse(ExtraerData(linea));
                }
                if (linea.Contains("ppTime"))
                {
                    maxTimePrecision = float.Parse(ExtraerData(linea));
                }
            }
        }
    }

    private void leerDataUser()
    {
        using (sysio.StreamReader reader = new sysio.StreamReader("dataUser.temp"))
        {
            string linea;
            while((linea = reader.ReadLine()) != null)
            {
                if (linea.Contains("Cedula"))
                {
                    cedula = ExtraerData(linea);
                }
                if (linea.Contains("Nombres"))
                {
                    nombres = ExtraerData(linea);
                }
                if (linea.Contains("Apellidos"))
                {
                    apellidos = ExtraerData(linea);
                }
                if (linea.Contains("Correo"))
                {
                    correo = ExtraerData(linea);
                }
                if (linea.Contains("Celular"))
                {
                    celular = ExtraerData(linea);
                }
            }
        }
    }
}
