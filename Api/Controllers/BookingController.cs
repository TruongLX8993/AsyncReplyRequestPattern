
using System.Text.Json;
using Api.DTOs;
using Domain;
using Domain.Entities;
using Domain.Repository;
using Microsoft.AspNetCore.Mvc;
namespace Api.Controllers;

[Route("/api/booking")]
public class BookingController : ControllerBase
{
    private readonly IRequestRepository _requestRepository;
    private readonly IResponseRepository _responseRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRequestQueue _requestQueue;


    public BookingController(IRequestRepository requestRepository,
        IResponseRepository responseRepository,
        IUnitOfWork unitOfWork,
        IRequestQueue requestQueue)
    {
        _requestRepository = requestRepository;
        _responseRepository = responseRepository;
        _unitOfWork = unitOfWork;
        _requestQueue = requestQueue;
    }
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Create([FromBody] BookingRequest bookingRequest)
    {
        var request = new Request
        {
            Id = Guid.NewGuid().ToString(),
            Body = JsonSerializer.Serialize(bookingRequest),
            Status = RequestStatus.Accepted,
        };

        try
        {
            _unitOfWork.Begin();
            await _requestRepository.Add(request);
            await _unitOfWork.Commit();
        }
        catch (Exception)
        {
            await _unitOfWork.Rollback();
            throw;
        }
        _requestQueue.Enqueue(request);
        return Accepted($"/api/booking/status/{request.Id}");
    }

    [HttpGet]
    [Route("status/{requestId}")]
    public async Task<IActionResult> GetStatus([FromRoute] string requestId)
    {
        var request = await _requestRepository.GetById(requestId);
        if (request == null)
            return NotFound("Not found request");

        return Ok(new
        {
            requestId,
            status = request.Status,
            resultUrl = request.Status == RequestStatus.Completed ? $"/api/booking/result/{requestId}" : string.Empty
        });
    }

    [HttpGet]
    [Route("result/{requestId}")]
    public async Task<IActionResult> GetResult([FromRoute] string requestId)
    {
        var response = await _responseRepository.GetByRequestId(requestId);
        if (response == null)
            return NotFound("Not found response");

        var bookingResponse = JsonSerializer.Deserialize<BookingResponse>(response.Body);
        return Ok(bookingResponse);

    }
}