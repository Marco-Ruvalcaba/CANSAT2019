using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/// SE UTILIZA PARA LAS GRAFICAS EN TIEMPO REAL
using LiveCharts;
using LiveCharts.Wpf;
/// KARINA - AL PARECER ES PARA LA CONEXION CON ARDUINO
using System.IO.Ports;
using LiveCharts.Defaults;

namespace CANSAT2019
{
    public partial class CANSAT2019 : Form
    {
        public CANSAT2019()
        {
            InitializeComponent();
            _Inicializar_Puertos();

            /// GRAFICAS /*
            GrafTemExt.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<float>
                    {

                    }, PointGeometrySize = 5
                }
            };
            GrafTemExt.AxisX.Add(new Axis
            {
                Title = "Tiempo"
            });
            GrafTemExt.AxisY.Add(new Axis
            {
                Title = "Temperatura Externa"
            });

            GrafTemInt.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<float>
                    {

                    }, PointGeometrySize = 5
                }
            };
            GrafTemInt.AxisX.Add(new Axis
            {
                Title = "Tiempo"
            });
            GrafTemInt.AxisY.Add(new Axis
            {
                Title = "Temperatura Interna"
            });

            GrafPresion.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<float>
                    {

                    }, PointGeometrySize = 5
                }
            };
            GrafPresion.AxisX.Add(new Axis
            {
                Title = "Tiempo"
            });
            GrafPresion.AxisY.Add(new Axis
            {
                Title = "Presion"
            });

            GrafHumedad.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<float>
                    {

                    }, PointGeometrySize = 5
                }
            };
            GrafHumedad.AxisX.Add(new Axis
            {
                Title = "Tiempo"
            });
            GrafHumedad.AxisY.Add(new Axis
            {
                Title = "Humedad"
            });

            GrafAltura.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<float>
                    {

                    }, PointGeometrySize = 5
                }
            };
            GrafAltura.AxisX.Add(new Axis
            {
                Title = "Tiempo"
            });
            GrafAltura.AxisY.Add(new Axis
            {
                Title = "Altura"
            });

            GrafLongitud.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<float>
                    {

                    }, PointGeometrySize = 5
                }
            };
            GrafLongitud.AxisX.Add(new Axis
            {
                Title = "Tiempo"
            });
            GrafLongitud.AxisY.Add(new Axis
            {
                Title = "Longitud"
            });

            GrafLatitud.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<float>
                    {

                    }, PointGeometrySize = 5
                }
            };
            GrafLatitud.AxisX.Add(new Axis
            {
                Title = "Tiempo"
            });
            GrafLatitud.AxisY.Add(new Axis
            {
                Title = "Latitud"
            });

            GrafVoltaje.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<float>
                    {

                    }, PointGeometrySize = 5
                }
            };
            GrafVoltaje.AxisX.Add(new Axis
            {
                Title = "Tiempo"
            });
            GrafVoltaje.AxisY.Add(new Axis
            {
                Title = "Voltaje"
            });

            GrafCalidad.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<float>
                    {

                    }, PointGeometrySize = 5
                }
            };
            GrafCalidad.AxisX.Add(new Axis
            {
                Title = "Tiempo"
            });
            GrafCalidad.AxisY.Add(new Axis
            {
                Title = "Calidad"
            }); 
        }
        ///----------------------------------------------------------------------------------------------------
        // ESTA FUNCION ENVIA A LA PANTALLA LOS DATOS RECIBIDOS 
        void TransformarTramaArreglo() //string Z
        {
            // VARIABLES QUE SE USARAN
            int O = 0;
            string aux = string.Empty;

            // TELEMETRIA 
            float TemExt = 00.00f;
            float TemInt = 00.00f;
            float PreAtm = 00.00f;
            float HumRel = 00.00f;
            float AltGps = 00.00f;
            float LonGps = 00.00f;
            float LatGps = 00.00f;
            float VolBat = 00.00f;
            float CalAir = 00.00f;
            //----------------------------------------------------------------------------------------------------
            // SE MANDA A LOS LABELS DONDE SE MOSTRARAN LOS DATOS
            lblTramaDatos.Text = Program.Trama;

            foreach (var z in Program.Trama)
            {
                if (z == '|')
                {
                    switch (O)
                    {
                        case 0: lblI.Text = aux; aux = string.Empty; O++; break;
                        case 1: TemExt = Convert.ToSingle(aux); lblValorExterna.Text = String.Format("{0:0,0.00}", TemExt); aux = string.Empty; O++; break;
                        case 2: TemInt = Convert.ToSingle(aux); lblValorInterna.Text = String.Format("{0:0,0.00}", TemInt); aux = string.Empty; O++; break;
                        case 3: PreAtm = Convert.ToSingle(aux); lblValorPresion.Text = String.Format("{0:0,0.00}", PreAtm); aux = string.Empty; O++; break;
                        case 4: HumRel = Convert.ToSingle(aux); lblValorHumedad.Text = String.Format("{0:0,0.00}", HumRel); aux = string.Empty; O++; break;
                        case 5: AltGps = Convert.ToSingle(aux); lblValorAlturaR.Text = String.Format("{0:0,0.00}", AltGps); aux = string.Empty; O++; break;
                        case 6: LonGps = Convert.ToSingle(aux); lblValorLngitud.Text = String.Format("{0:0,0.00}", LonGps); aux = string.Empty; O++; break;
                        case 7: LatGps = Convert.ToSingle(aux); lblValorLatitud.Text = String.Format("{0:0,0.00}", LatGps); aux = string.Empty; O++; break;
                        case 8: VolBat = Convert.ToSingle(aux); lblValorVoltaje.Text = String.Format("{0:0,0.00}", VolBat); aux = string.Empty; O++; break;
                        case 9: CalAir = Convert.ToSingle(aux); lblValorCalidad.Text = String.Format("{0:0,0.00}", CalAir); aux = string.Empty; O++; break;
                    }
                }
                else
                {
                    aux = aux + z;
                }
            }

            //----------------------------------------------------------------------------------------------------
            // SE GUARDAN LOS DATOS EN LA LISTA
            Program.ListaTelemetria.Add(new Program.Telemetria( TemExt, TemInt, PreAtm, HumRel, AltGps, LonGps, LatGps, VolBat, CalAir));

            //----------------------------------------------------------------------------------------------------
            // SE GRAFICAN LAS VARIABLES
            GrafTemExt.Series[0].Values.Add(TemExt);
            GrafTemInt.Series[0].Values.Add(TemInt);
            GrafPresion.Series[0].Values.Add(PreAtm);
            GrafHumedad.Series[0].Values.Add(HumRel);
            GrafAltura.Series[0].Values.Add(AltGps);
            GrafLongitud.Series[0].Values.Add(LonGps);
            GrafLatitud.Series[0].Values.Add(LatGps);
            GrafVoltaje.Series[0].Values.Add(VolBat);
            GrafCalidad.Series[0].Values.Add(CalAir);

            /// EXPERIMENTO PARA QUITAR EL PRIMER ELEMENTO
            GrafTemExt.Series[0].Values.Remove(GrafTemExt.Series);
        }

        public void _Inicializar_Puertos()
        {
            try
            {
                Program.Puertos = SerialPort.GetPortNames();

                foreach (string mostrar in Program.Puertos)
                {
                    CmbBxArduino.Items.Add(mostrar);
                }

                if (CmbBxArduino.Items.Count == 0)
                {
                    LblConexion.Text = "NADA CONECTADO"; //NO HAY NADA CONECTADO
                }
            }
            catch
            {
                MessageBox.Show("NO SE PUEDEN OBTENER LOS PUERTOS", "Error");
            }
        }

        private void PuertoSerial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Program.Trama = PuertoSerial.ReadLine();
            TransformarTramaArreglo();
        }

        private void CmbBxArduino_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PuertoSerial.Close();
                PuertoSerial.Dispose();
                Program.PuertoSeleccionado = CmbBxArduino.Text;
                PuertoSerial.BaudRate = 9600;
                PuertoSerial.PortName = Program.PuertoSeleccionado;
                PuertoSerial.Open();
                CheckForIllegalCrossThreadCalls = false;
                if (PuertoSerial.IsOpen)
                {
                    LblConexion.Text = "PUERTO CONECTADO";
                }
                else
                {
                    return;
                }
            }
            catch
            {
                MessageBox.Show("NO SE LOGRO CONECTAR EL PUERTO", "Error");
            }
        }

        /// BOTONES PRINCIPALES
        // BOTON INICIAL --------------------------------------------------
        private void BtnIniciar_Click(object sender, EventArgs e)
        {
        }
        // BOTON TERMINAR --------------------------------------------------
        private void BtnTerminar_Click(object sender, EventArgs e)
        {
            // CERRAR EL PUERTO
            PuertoSerial.Close();
            // LIBERAR LOS RECURSOS
            PuertoSerial.Dispose(); 
            // ALMACENAR DATOS EN EXCEL
            // Program._Mandar_Datos_a_Excel();
        }
        // BOTON SALIR --------------------------------------------------
        private void BtnSalir_Click(object sender, EventArgs e)
        {
            // CERRAR EL PUERTO
            PuertoSerial.Close();
            // LIBERAR LOS RECURSOS
            PuertoSerial.Dispose();
            // SE CIERRA LA VENTANA Y LA EJECUCION DEL PROGRAMA            
            Close();
        }
        
        private void GrafTemExt_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {
    
        }
        private void GrafTemInt_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
        private void GrafPresion_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
        private void GrafHumedad_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
        private void GrafAltura_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
        private void GrafLongitud_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
        private void GrafLatitud_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
        private void GrafVoltaje_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
        private void GrafCalidad_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void lblValorLatitud_Click(object sender, EventArgs e)
        {

        }

        private void LblAltura_Click(object sender, EventArgs e)
        {

        }

        private void lblValorAlturaR_Click(object sender, EventArgs e)
        {

        }

        private void LblPosicion_Click(object sender, EventArgs e)
        {

        }

        private void LblLatitud_Click(object sender, EventArgs e)
        {

        }

        private void lblValorLngitud_Click(object sender, EventArgs e)
        {

        }

        private void LblLongitud_Click(object sender, EventArgs e)
        {

        }

        private void LblConexion_Click(object sender, EventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LblInicio_Click(object sender, EventArgs e)
        {

        }

        private void lblJ_Click(object sender, EventArgs e)
        {

        }

        private void LblFinal_Click(object sender, EventArgs e)
        {

        }

        private void lblI_Click(object sender, EventArgs e)
        {

        }

        private void LblTemInterna_Click(object sender, EventArgs e)
        {

        }

        private void lblValorInterna_Click(object sender, EventArgs e)
        {

        }

        private void lblValorExterna_Click(object sender, EventArgs e)
        {

        }

        private void lblValorPresion_Click(object sender, EventArgs e)
        {

        }

        private void lblValorHumedad_Click(object sender, EventArgs e)
        {

        }

        private void LblHumedad_Click(object sender, EventArgs e)
        {

        }

        private void LblTemExterna_Click(object sender, EventArgs e)
        {

        }

        private void LblTemperatura_Click(object sender, EventArgs e)
        {

        }

        private void LblPresionATM_Click(object sender, EventArgs e)
        {

        }

        private void lblValorVoltaje_Click(object sender, EventArgs e)
        {

        }

        private void LblVoltaje_Click(object sender, EventArgs e)
        {

        }

        private void lblValorCalidad_Click(object sender, EventArgs e)
        {

        }

        private void LblCalidad_Click(object sender, EventArgs e)
        {

        }

        private void LabelTitulo_Click(object sender, EventArgs e)
        {

        }

        private void CANSAT2019_Load(object sender, EventArgs e)
        {

        }
    }
}
