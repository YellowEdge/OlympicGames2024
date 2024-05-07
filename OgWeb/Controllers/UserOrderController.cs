using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OgWeb.Repositories;

namespace OgWeb.Controllers;

[Authorize]
public class UserOrderController : Controller
{
    private readonly IUserOrderRepository _userOrderRepo;
    private readonly IQRCodeGeneratorRepository _userQRCodeRepo;

    public UserOrderController(IUserOrderRepository userOrderRepo, IQRCodeGeneratorRepository userQRCodeRepo)
    {
        _userOrderRepo = userOrderRepo;
        _userQRCodeRepo = userQRCodeRepo;
    }
    public async Task<IActionResult> UserOrders()
    {
        var orders = await _userOrderRepo.UserOrders();
        return View(orders);
    }

    [HttpPost]
    public IActionResult UserEticket(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return BadRequest("Invalid status !!");
        }
        else
        {
            bool paymentStatus = _userOrderRepo.GetPaymentStatusByKey(text);
            if (paymentStatus == false) return BadRequest("Check your payment status !!");
        }

        byte[] QRCodeAsBytes = _userQRCodeRepo.GenerateQRCode(text);
        string QrCodeAsImageBase64 = $"data:image/png;base64,{Convert.ToBase64String(QRCodeAsBytes)}";

        GenerateQRCodeModel model = new GenerateQRCodeModel
        {
            QRCodeImageUrl = QrCodeAsImageBase64
        };

        return View(model);
    }
}