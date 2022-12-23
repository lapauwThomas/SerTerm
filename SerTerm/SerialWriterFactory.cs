using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerTerm
{
    internal class SerialWriterFactory
    {
        private SerialPort serialPort;
        public SerialWriterFactory()
        {
            serialPort = new SerialPort();
        }

        public void SetPort(string port)
        {
            serialPort.PortName = port;
        }
        public void FromJsonConfig(string jsonconfig)
        {

        }

        public void SettingsFromSerconf(string serconf)
        {

        }

        public void SetBaud(string baudrate)
        {
            int baud;
            baud = int.Parse(baudrate);
            SetBaud(baud);
        }
        public void SetBaud(int baudrate)
        {
            serialPort.BaudRate = baudrate;
        }

        public void SetOutFile(string filepath, string filemode)
        {

        }

        public void SetTimestamp()
        {

        }
        public void SetTimestamp(string format)
        {

        }

        public void PrintMode()
        {

        }

        public void SetPrintLineTimeout(int millis)
        {

        }


        public void SetEscapeCodeHandeling(bool strip)
        {

        }

        public SerialPortClass Create()
        {


            SerialPortClass portInstance = new SerialPortClass();





            return portInstance;
        }

    }
}
