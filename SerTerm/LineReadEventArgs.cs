namespace SerTerm;

public class LineReadEventArgs : EventArgs
{
    public readonly DateTime Timestamp;
    public readonly byte[] Bytes;
    public readonly bool Timeout;

    public LineReadEventArgs(byte[] buffer, int length, bool timeout)
    {
        Bytes = buffer.SubArray(0, length);
        Timestamp = DateTime.Now;
        this.Timeout = timeout;
    }
}