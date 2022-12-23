using System;
using System.IO.Ports;


public class Program
{
    
    string InData;
    static SerialPort myComPort;
    public static void Main()
    {
        // Init Com Port
        myComPort = new SerialPort("COM4", 19200, Parity.None, 8, StopBits.One);
        myComPort.Handshake = Handshake.None;
        myComPort.DtrEnable = true;

        // Event Handler
        myComPort.DataReceived += new SerialDataReceivedEventHandler(DataRecievedHandler);
        myComPort.Open();
        
        Console.WriteLine("Press any Key to Continue");
        Console.WriteLine();
        Console.ReadKey();
        myComPort.Close();
        
    }

    static void DataRecievedHandler(object sender, SerialDataReceivedEventArgs e){
       
        string indata = myComPort.ReadLine();
        Console.WriteLine(indata);
    }

}