namespace NumberToWords.Tests;

public class NumberToWordsConverterTests
{
    private readonly NumberToWords.API.NumberToWords _converter;

    public NumberToWordsConverterTests()
    {
        _converter = new NumberToWords.API.NumberToWords();
    }

    [Fact]
    public void Convert_SimpleAmount_ReturnCorrectWords()
    {
        string input = "123.45";
        string expected = "ONE HUNDRED AND TWENTY THREE DOLLARS AND FORTY FIVE CENTS";

        string result = _converter.Convert(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Convert_NullInput()
    {
        Assert.Throws<ArgumentException>(() => _converter.Convert(""));
    }

    [Fact]
    public void Convert_WhiteSpaceInput()
    {
        Assert.Throws<ArgumentException>(() => _converter.Convert(" "));
    }

    [Fact]
    public void Convert_StringInput()
    {
        Assert.Throws<ArgumentException>(() => _converter.Convert("abc "));
    }

    [Fact]
    public void Convert_CommaInput()
    {
        Assert.Throws<ArgumentException>(() => _converter.Convert("100,100"));
    }

    [Fact]
    public void Convert_DecimalPointInput()
    {
        Assert.Throws<ArgumentException>(() => _converter.Convert("678.89.9"));
    }

    [Fact]
    public void Convert_ZeroInput()
    {
        string input = "0";
        string expected = "ZERO DOLLARS";

        string result = _converter.Convert(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Convert_NegativeInput()
    {
        Assert.Throws<ArgumentException>(() => _converter.Convert("-678.89"));
    }

    [Fact]
    public void Convert_MaxInput()
    {
        string input = "999999999999999";
        string expected = "NINE HUNDRED AND NINETY NINE TRILLION NINE HUNDRED AND NINETY NINE BILLION NINE HUNDRED AND NINETY NINE MILLION NINE HUNDRED AND NINETY NINE THOUSAND NINE HUNDRED AND NINETY NINE DOLLARS";

        string result = _converter.Convert(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Convert_MultipleZeroesInput()
    {
        string input = "90000009";
        string expected = "NINETY MILLION NINE DOLLARS";

        string result = _converter.Convert(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Convert_OneCent_ReturnsSingularCent()
    {
        string result = _converter.Convert("0.01");
        Assert.Equal("ZERO DOLLARS AND ONE CENT", result);
    }

    [Fact]
    public void Convert_CentsOnly_ReturnsCorrectly()
    {
        string result = _converter.Convert("0.50");
        Assert.Equal("ZERO DOLLARS AND FIFTY CENTS", result);
    }

    [Fact]
    public void Convert_ExcessiveDecimalPlaces_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => _converter.Convert("123.456"));
    }
    
}
