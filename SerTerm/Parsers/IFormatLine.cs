namespace SerTerm.Parsers;

public interface IFormatLine
{

    string FormatLine(byte[] line);

    string FormatByte(byte databyte);


}