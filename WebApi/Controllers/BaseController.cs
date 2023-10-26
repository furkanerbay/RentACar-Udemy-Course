using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class BaseController : ControllerBase
{
    //Bunu sadece bunu inherit edenler kullanabilsin. -> Protected
    //Mediator var mı varsa onu getir.
    private IMediator? _mediator;
    protected IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    //Yoksa injectionlara bak bulduğun noktada bunu set et.
    //Injection IoC ortamından bana injection karşılığını ver.
}
