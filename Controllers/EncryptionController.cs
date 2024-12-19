using Microsoft.AspNetCore.Mvc;

namespace software_engineering_product_flowerpower.Controllers;

[Route("api")]
[Controller]
public class EncryptionController : Controller
{
    private readonly string _secretKey =
        "cpVCSsE5teKgTZGdmPZAky2xBx9Juym7Pb10ZydustQfDdE3bIiCie88xaP1kVW3nIWqn1/4W+dnbxg00waM9gZTeQkZpszXPyydMMtAOnUxs3DcNdJG0dkAIyqc9DvdbD+8Hf2MUnEeSCS8SacxsXHPGds6/jFyLjyMBtTyLoZUsIGsDq8sFXGGko+L73C9";
    
    [Route("secret-key")]
    [HttpGet]
    public IActionResult GetSecretKey()
    {
        return Ok(new { secretKey = _secretKey});
    }
}

