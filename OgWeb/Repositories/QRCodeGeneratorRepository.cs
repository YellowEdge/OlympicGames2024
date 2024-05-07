using QRCoder;

namespace OgWeb.Repositories;

public class QRCodeGeneratorRepository : IQRCodeGeneratorRepository
{
    public byte[] GenerateQRCode(string text)
    {
        byte[] QRCode = new byte[0];
        if (!string.IsNullOrEmpty(text))
        {
            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData data = qRCodeGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            BitmapByteQRCode bitmap = new BitmapByteQRCode(data);
            QRCode = bitmap.GetGraphic(10);
        }
        return QRCode;
    }
}
