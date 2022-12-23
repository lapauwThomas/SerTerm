using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using IronPython.Modules;
using static IronPython.Modules.ArrayModule;

namespace SerTerm;

internal class SerialPortClass
{
    public SerialPortClass(SerialPort port, ISerialPortReader reader)
    {

    }

    public void WriteLine(string line)
    {

    }

    public void Write(string text)
    {

    }

    public void Write(byte[] bytes)
    {

    }

    public void Write(byte data)
    {

    }

    public void OnLineReceived(object sender, LineReadEventArgs args)
    {

    }


    private void OutputLine(string line)
    {

    }

}

public static class ArrayExtensions
{
    public static T[] SubArray<T>(this T[] array, int offset, int length)
    {
        T[] result = new T[length];
        Array.Copy(array, offset, result, 0, length);
        return result;
    }
}

