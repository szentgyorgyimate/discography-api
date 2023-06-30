using Microsoft.AspNetCore.Mvc;
using WebAPI.Common.Mappers;
using WebAPI.Common.Requests.Member;
using WebAPI.Common.Responses;
using WebAPI.Common.Responses.Member;
using WebAPI.Interfaces;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MemberController : ControllerBase
{
    private readonly IMemberRepository _memberRepository;

    public MemberController(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResponse<List<MemberDetailedData>>))]
    public IResult GetAllMembers()
    {
        var members = _memberRepository.GetAll();
        var memberData = members.Select(m => m.ToMemberDetailedData()).ToList();
        var response = new DataResponse<List<MemberDetailedData>>(memberData);

        return Results.Ok(response);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(FailResponse))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResponse<MemberDetailedData>))]
    public IResult GetMember([FromRoute] int id)
    {
        var member = _memberRepository.GetById(id);

        if (member == null)
        {
            return Results.NotFound(new FailResponse("Member does not exist."));
        }

        var response = new DataResponse<MemberDetailedData>(member.ToMemberDetailedData());

        return Results.Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DataResponse<CreateMemberData>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(FailResponse))]
    public IResult CreateBand([FromBody] CreateMemberRequest request)
    {
        var failResponse = ValidateFields(request.Name);

        if (failResponse != null)
        {
            return Results.BadRequest(failResponse);
        }

        var newMemberId = _memberRepository.Add(request.Name);
        var response = new DataResponse<CreateMemberData>(new CreateMemberData() { Id = newMemberId });

        return Results.Created($"api/member/{newMemberId}", response);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(FailResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(FailResponse))]
    public IResult UpdateMember([FromRoute] int id, [FromBody] UpdateMemberRequest request)
    {
        if (!_memberRepository.IsMemberExist(id))
        {
            return Results.NotFound(new FailResponse("Member does not exist."));
        }

        var failResponse = ValidateFields(request.Name);

        if (failResponse != null)
        {
            return Results.BadRequest(failResponse);
        }

        _memberRepository.Update(id, request.Name);

        return Results.Ok(new SuccessResponse());
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(FailResponse))]
    public IResult DeleteMember([FromRoute] int id)
    {
        if (!_memberRepository.IsMemberExist(id))
        {
            return Results.NotFound(new FailResponse("Member does not exist."));
        }

        _memberRepository.Delete(id);

        return Results.Ok(new SuccessResponse());
    }

    private FailResponse ValidateFields(string name)
    {
        FailResponse failResponse = null;

        if (string.IsNullOrWhiteSpace(name))
        {
            failResponse = new FailResponse("One or more validation error occured.")
            {
                Data = new Dictionary<string, string[]>(),
            };

            failResponse.Data.Add("name", new string[] { "Name is required." });
        }

        return failResponse;
    }
}
