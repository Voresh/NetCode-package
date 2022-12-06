using FluentAssertions;
using Xunit;

namespace NetCode.UnitTests;

public class ByteReaderAndWriterTests
{
    private byte @byte = 0b_11110000;
    private ushort @short = 0b_11110000_00001111;
    private uint @int = 0b_10101010_01010101_11110000_00001111;
    private ulong @long = 0b_11111111_00000000_11001100_00110011_10101010_01010101_11110000_00001111;
    
    [Fact]
    public void WriteAndReadTheSameData()
    {
        var array = new byte[15];
        var byteWriter = new ByteWriter(array);
        
        byteWriter.Write(@byte); // 1
        byteWriter.Write(@short); // 2
        byteWriter.Write(@int); // 4
        byteWriter.Write(@long); // 8

        byteWriter.Count.Should().Be(15);

        var data = byteWriter.Array;
        data.Should().BeSameAs(array);

        var byteReader = new ByteReader(data);

        byteReader.ReadByte().Should().Be(@byte);
        byteReader.ReadUShort().Should().Be(@short);
        byteReader.ReadUInt().Should().Be(@int);
        byteReader.ReadULong().Should().Be(@long);
    }
}