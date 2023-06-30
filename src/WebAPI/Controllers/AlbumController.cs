using Microsoft.AspNetCore.Mvc;
using WebAPI.Common.Mappers;
using WebAPI.Common.Requests.Album;
using WebAPI.Common.Responses;
using WebAPI.Common.Responses.Album;
using WebAPI.Interfaces;

namespace MusicAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumController : ControllerBase
{
    private readonly IAlbumRepository _albumRepository;
    private readonly IAlbumTypeRepository _albumTypeRepository;
    private readonly IBandRepository _bandRepository;

    public AlbumController(IAlbumRepository albumRepository, IBandRepository bandRepository, IAlbumTypeRepository albumTypeRepository)
    {
        _albumRepository = albumRepository;
        _bandRepository = bandRepository;
        _albumTypeRepository = albumTypeRepository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResponse<List<AlbumData>>))]
    public IResult GetAllAlbums([FromQuery] GetAllAlbumRequest request)
    {
        var albums = _albumRepository.GetAll(request.BandId, request.TypeId, request.ReleaseFrom, request.ReleaseTo);
        var albumData = albums.Select(a => a.ToAlbumData()).ToList();

        return Results.Ok(new DataResponse<List<AlbumData>>(albumData));
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(FailResponse))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResponse<AlbumData>))]
    public IResult GetAlbum([FromRoute] int id)
    {
        var album = _albumRepository.GetById(id);

        if (album == null)
        {
            return Results.NotFound(new FailResponse("Album does not exist."));
        }

        return Results.Ok(new DataResponse<AlbumData>(album.ToAlbumData()));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DataResponse<CreateAlbumData>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(FailResponse))]
    public IResult CreateAlbum([FromBody] CreateAlbumRequest request)
    {
        var failResponse = ValidateFields(request.Bandid, request.TypeId, request.Name);

        if (failResponse != null)
        {
            return Results.BadRequest(failResponse);
        }

        var newAlbumId = _albumRepository.Add(request.Bandid, request.TypeId, request.Name, request.ReleaseDate);
        var response = new DataResponse<CreateAlbumData>(new CreateAlbumData() { Id = newAlbumId });

        return Results.Created($"api/album/{newAlbumId}", response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(FailResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(FailResponse))]
    [Route("{id}")]
    public IResult UpdateAlbum([FromRoute] int id, [FromBody] UpdateAlbumRequest request)
    {
        if (!_albumRepository.IsExists(id))
        {
            return Results.NotFound(new FailResponse("Album does not exist."));
        }

        var failResponse = ValidateFields(request.Bandid, request.TypeId, request.Name);

        if (failResponse != null)
        {
            return Results.BadRequest(failResponse);
        }

        _albumRepository.Update(id, request.Bandid, request.TypeId, request.Name, request.ReleaseDate);

        return Results.Ok(new SuccessResponse());
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(FailResponse))]
    [Route("{id}")]
    public IResult DeleteAlbum([FromRoute] int id)
    {
        if (!_albumRepository.IsExists(id))
        {
            return Results.NotFound(new FailResponse("Album does not exist."));
        }

        _albumRepository.Delete(id);

        return Results.Ok(new SuccessResponse());
    }

    private FailResponse ValidateFields(int bandId, int typeId, string name)
    {
        FailResponse failResponse = null;

        var isValidBand = _bandRepository.IsBandExist(bandId);
        var isValidType = _albumTypeRepository.IsAlbumTypeExist(typeId);
        var isValidName = !string.IsNullOrWhiteSpace(name);

        if (!isValidBand || !isValidType || !isValidName)
        {
            failResponse = new FailResponse("One or more validation error occured.")
            {
                Data = new Dictionary<string, string[]>(),
            };

            if (!isValidBand)
            {
                failResponse.Data.Add("bandId", new string[] { "Invalid band id." });
            }

            if (!isValidType)
            {
                failResponse.Data.Add("typeId", new string[] { "Invalid album type id." });
            }

            if (!isValidName)
            {
                failResponse.Data.Add("name", new string[] { "Name is required." });
            }
        }

        return failResponse;
    }
}
