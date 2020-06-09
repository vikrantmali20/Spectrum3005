using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using Microsoft.VisualBasic;


namespace Spectrum.BL
{
  public  class clsTwoLineDisplay
    {
        public static SerialPort _20x2Line;
        public static void ClearDisplay20x2Line(string portname)
        {
            try
            {
                _20x2Line = new SerialPort();
                _20x2Line.PortName = portname;
                _20x2Line.BaudRate = 9600;
                _20x2Line.DataBits = 8;
                _20x2Line.StopBits = StopBits.One;
                _20x2Line.Parity = Parity.None;
                _20x2Line.ReadTimeout = 5000;
                _20x2Line.Handshake = Handshake.None;
                _20x2Line.WriteTimeout = 500;

                if (_20x2Line.IsOpen == false) // Checks whether port is already OPEN or Close 
                    _20x2Line.Open();          // If COM port is not opened then OPEN for communication

                _20x2Line.Write("\x0c");                    // 0x0c hex value to clear the display before sending any message to Display
                //_20x2Line.Write(line1String + line2String); // writes above message to Display

                if (_20x2Line.IsOpen == true)               // Checks whether port is already OPEN, If OPEN then Close it before exiting the function
                    _20x2Line.Close();

                _20x2Line.Dispose();
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void ClientNameDisplay20x2Line(string ClientName, string line2String, string portname)
        {
            try
            {
                _20x2Line = new SerialPort();
                _20x2Line.PortName = portname;
                _20x2Line.BaudRate = 9600;
                _20x2Line.DataBits = 8;
                _20x2Line.StopBits = StopBits.One;
                _20x2Line.Parity = Parity.None;
                _20x2Line.ReadTimeout = 5000;
                _20x2Line.Handshake = Handshake.None;
                _20x2Line.WriteTimeout = 500;

                if (_20x2Line.IsOpen == false) // Checks whether port is already OPEN or Close 
                    _20x2Line.Open();          // If COM port is not opened then OPEN for communication

                //_20x2Line.Write("\x0c");                    // 0x0c hex value to clear the display before sending any message to Display
                _20x2Line.Write(ClientName + line2String); // writes above message to Display

                if (_20x2Line.IsOpen == true)               // Checks whether port is already OPEN, If OPEN then Close it before exiting the function
                    _20x2Line.Close();

                _20x2Line.Dispose();
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
