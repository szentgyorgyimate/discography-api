using Microsoft.AspNetCore.Mvc;
using WebAPI.Common.Mappers;
using WebAPI.Common.Requests.Band;
using WebAPI.Common.Responses;
using WebAPI.Common.Responses.Band;
using WebAPI.Interfaces;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BandController : ControllerBase
{
    private readonly IBandRepository _bandRepository;
    private readonly IMemberRepository _memberRepository;

    public BandController(IBandRepository bandRepository, IMemberRepository memberRepository)
    {
        _bandRepository = bandRepository;
        _memberRepository = memberRepository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResponse<List<BandDetailedData>>))]
    public IResult GetAllBands()
    {
        var bands = _bandRepository.GetAll();
        var bandData = bands.Select(b => b.ToBandDetailedData()).ToList();

        return Results.Ok(new DataResponse<List<BandDetailedData>>(bandData));
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(FailResponse))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResponse<BandDetailedData>))]
    public IResult GetBand([FromRoute] int id)
    {
        var band = _bandRepository.GetById(id);

        if (band == null)
        {
            return Results.NotFound(new FailResponse("Band does not exist."));
        }

        return Results.Ok(new DataResponse<BandDetailedData>(band.ToBandDetailedData()));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DataResponse<CreateBandData>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(FailResponse))]
    public IResult CreateBand([FromBody] CreateBandRequest request)
    {
        var failResponse = ValidateFields(request.Name, request.Genre, request.CountryOfOrigin);

        if (failResponse != null)
        {
            return Results.BadRequest(failResponse);
        }

        var newBandId = _bandRepository.Add(request.Name, request.Genre, request.CountryOfOrigin, request.FormedIn);
        var response = new DataResponse<CreateBandData>(new CreateBandData() { Id = newBandId });

        return Results.Created($"api/band/{newBandId}", response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(FailResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(FailResponse))]
    [Route("{id}/member")]
    public IResult AddMemberToBand([FromRoute] int id, [FromBody] AddMemberToBandRequest request)
    {
        if (!_bandRepository.IsBandExist(id))
        {
            return Results.NotFound(new FailResponse("Band does not exist."));
        }

        if (!_memberRepository.IsMemberExist(request.MemberId))
        {
            return Results.NotFound(new FailResponse("Member does not exist."));
        }

        if (_bandRepository.IsBandHasMember(id, request.MemberId))
        {
            return Results.BadRequest(new FailResponse("Member is already in the band."));
        }

        _bandRepository.AddMemberToBand(id, request.MemberId);

        return Results.Created($"api/band/{id}", new SuccessResponse());
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(FailResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(FailResponse))]
    public IResult UpdateBand([FromRoute] int id, [FromBody] UpdateBandRequest request)
    {
        if (!_bandRepository.IsBandExist(id))
        {
            return Results.NotFound(new FailResponse("Band does not exist."));
        }

        var failResponse = ValidateFields(request.Name, request.Genre, request.CountryOfOrigin);

        if (failResponse != null)
        {
            return Results.BadRequest(failResponse);
        }

        _bandRepository.Update(id, request.Name, request.Genre, request.CountryOfOrigin, request.FormedIn);

        return Results.Ok(new SuccessResponse());
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(FailResponse))]
    public IResult DeleteBand([FromRoute] int id)
    {
        if (!_bandRepository.IsBandExist(id))
        {
            return Results.NotFound(new FailResponse("Band does not exist."));
        }

        _bandRepository.Delete(id); 
        
        return Results.Ok(new SuccessResponse());
    }

    [HttpDelete]
    [Route("{id}/member")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(FailResponse))]
    public IResult DeleteMemberFromBand([FromRoute] int id, [FromBody] DeleteMemberFromBandRequest request)
    {
        if (!_bandRepository.IsBandExist(id))
        {
            return Results.NotFound(new FailResponse("Band does not exist."));
        }

        if (!_memberRepository.IsMemberExist(request.MemberId))
        {
            return Results.NotFound(new FailResponse("Member does not exist."));
        }

        if (!_bandRepository.IsBandHasMember(id, request.MemberId))
        {
            return Results.BadRequest(new FailResponse("Member is not in the band."));
        }

        _bandRepository.DeleteMemberFromBand(id, request.MemberId);

        return Results.Ok(new SuccessResponse());
    }

    private FailResponse ValidateFields(string name, string genre, string countryOfOrigin)
    {
        FailResponse failResponse = null;

        var isValidName = !string.IsNullOrWhiteSpace(name);
        var isValidGenre = !string.IsNullOrWhiteSpace(genre);
        var isValidCountryOfOrigin = !string.IsNullOrWhiteSpace(countryOfOrigin);

        if (!isValidName || !isValidGenre || !isValidCountryOfOrigin)
        {
            failResponse = new FailResponse("One or more validation error occured.")
            {
                Data = new Dictionary<string, string[]>()
            };

            if (!isValidName)
            {
                failResponse.Data.Add("name", new string[] { "Name is required." });
            }

            if (!isValidGenre)
            {
                failResponse.Data.Add("genre", new string[] { "Genre is required." });
            }

            if (!isValidCountryOfOrigin)
            {
                failResponse.Data.Add("countryOfOrigin", new string[] { "Country of origin is required." });
            }
        }

        return failResponse;
    }
}
