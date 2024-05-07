namespace OgWeb.Repositories;

public interface IQRCodeGeneratorRepository
{
    byte[] GenerateQRCode(string text);
}
