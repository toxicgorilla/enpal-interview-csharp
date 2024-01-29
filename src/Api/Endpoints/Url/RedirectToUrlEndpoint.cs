using Api.Endpoints.Url.Requests;
using MediatR;
using UrlShortenerService.Application.Url.Commands;
using IMapper = AutoMapper.IMapper;

namespace Api.Endpoints.Url;

public class RedirectToUrlSummary : Summary<RedirectToUrlEndpoint>
{
    public RedirectToUrlSummary()
    {
        Summary = "Redirect to the original url from the short url";
        Description =
            "This endpoint will redirect to the original url from the short url. If the short url is not found, it will return a 404.";
        Response(404, "No short url found.");
        Response(500, "Internal server error.");
    }
}

public class RedirectToUrlEndpoint(ISender mediator, IMapper mapper)
    : BaseEndpoint<RedirectToUrlRequest>(mediator, mapper)
{
    public override void Configure()
    {
        base.Configure();
        Get("u/{Id}");
        AllowAnonymous();
        Description(
            d => d.WithTags("Url")
        );
        Summary(new RedirectToUrlSummary());
    }

    public override async Task HandleAsync(RedirectToUrlRequest req, CancellationToken ct)
    {
        var result = await Mediator.Send(
            new RedirectToUrlCommand
            {
                Id = req.Id
            },
            ct
        );
        await SendRedirectAsync(result, false, ct);
    }
}
