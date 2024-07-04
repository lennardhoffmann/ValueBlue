using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using VB.API;
using VB.API.Logic;
using VB.Data.Models;
using VB.Data.Repositories;
using VB.Tests.ObjectBuilders;

namespace VB.Tests
{
    public class DatabaseQueryServiceTests
    {
        private readonly Mock<IFilmRequestRepository> _repository;
        private readonly IMapper _mapper;
        private readonly DatabaseQueryService _sut;

        public DatabaseQueryServiceTests()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            _repository = new Mock<IFilmRequestRepository>();
            _mapper = mappingConfig.CreateMapper();

            _sut = new DatabaseQueryService(_mapper, _repository.Object);
        }

        [Fact]
        public async void AddSearchRequestRecord_ValidProperties_SuccessfullyInsertsRecord()
        {
            var validProperties = new RequestPropertiesBuilder().Build();

            _repository.Setup(repo => repo.AddSearchRequestAsync(It.IsAny<FilmRequest>())).ReturnsAsync(true);

            await _sut.AddSearchRequestRecord(validProperties);
        }

        [Fact]
        public async void GetAllFilmRquest_Returns_ListFilmRequests()
        {
            var filmRequestRecord = new FilmRequestRecordBuilder().Build();
            var filmList = new List<FilmRequest>
            {
               filmRequestRecord
            };

            _repository.Setup(repo => repo.GetAllSearchRequestsAsync()).ReturnsAsync(filmList);

            var result = await _sut.GetAllFilmRequests();

            result.Count.Should().Be(1);

            var film = result.First();
            film.Search_Token.Should().Be(filmRequestRecord.Search_Token);
            film.imdbID.Should().Be(filmRequestRecord.imdbID);
        }

        [Fact]
        public async void GetFilmRequestByFilmName_ValidFilmName_ReturnsValidRequest()
        {
            const string filmName = "Police Academy";
            var filmRequestRecord = new FilmRequestRecordBuilder().Build();

            _repository.Setup(repo => repo.GetSearchRequestBySearchTokenAsync(filmName)).ReturnsAsync(filmRequestRecord);

            var result = await _sut.GetFilmRequestByFilmName(filmName);

            result.Search_Token.Should().Be(filmName);
        }

        [Fact]
        public async void GetFilmRequestByFilmName_InvalidFilmName_ThrowsException()
        {
            const string filmName = "Chunky Monkey";
            var nullResponse = (FilmRequest)null;

            _repository.Setup(repo => repo.GetSearchRequestBySearchTokenAsync(filmName)).ReturnsAsync(nullResponse);

            await _sut.Invoking(sut => sut.GetFilmRequestByFilmName(filmName))
                       .Should()
                       .ThrowAsync<Exception>($"Film with title {filmName} not found");
        }

        [Fact]
        public async void GetFilmRequestsForDatePeriod_ValidDates_ReturnsRequestRecordList()
        {
            var startDate = DateTime.Now.ToString("yyyy-MM-dd");
            var endDate = DateTime.Now.ToString("yyyy-MM-dd");

            var filmRequestRecord = new FilmRequestRecordBuilder().Build();
            var filmList = new List<FilmRequest>
            {
               filmRequestRecord
            };

            _repository.Setup(repo => repo.GetSearchRequestByDateRangeAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(filmList);

            var result = await _sut.GetFilmRequestsForDatePeriod(startDate, endDate);
            result.Count.Should().Be(1);

            var filmRequest = result.First();
            filmRequest.TimeStamp.Date.ToString("yyyy-MM-dd").Should().Be(startDate);
            filmRequest.Search_Token.Should().Be("Police Academy");
        }

        [Fact]
        public async void GetFilmRequestsForDatePeriod_AnyInvalidDate_ThrowsException()
        {
            var startDate = "bad date man";
            var endDate = DateTime.Now.ToString("yyyy-MM-dd");

            await _sut.Invoking(sut => sut.GetFilmRequestsForDatePeriod(startDate, endDate))
                      .Should()
                      .ThrowAsync<BadHttpRequestException>("Invalid format for start date provided");
        }

        [Fact]
        public async void DeleteFilmRequestByFilmName_ValidFilmName_DeletesRecord()
        {
            const string filmName = "Police Academy";

            _repository.Setup(repo => repo.DeleteSearchRequestBtSearchTokenAsync(filmName)).ReturnsAsync(true);

            await _sut.DeleteFilmRequestByFilmName(filmName);
        }

    }
}