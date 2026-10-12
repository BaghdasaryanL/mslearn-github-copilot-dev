using Library.ApplicationCore;
using Library.ApplicationCore.Entities;
using Library.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using NSubstitute;

namespace Library.UnitTests.Infrastructure.JsonLoanRepositoryTests;

public class GetLoanTest
{
	private readonly ILoanRepository _mockLoanRepository;
	private readonly JsonLoanRepository _jsonLoanRepository;
	private readonly IConfiguration _configuration;
	private readonly JsonData _jsonData;

	public GetLoanTest()
	{
		_mockLoanRepository = Substitute.For<ILoanRepository>();
		_configuration = new ConfigurationBuilder().Build();
		_jsonData = new JsonData(_configuration);
		_jsonLoanRepository = new JsonLoanRepository(_jsonData);
	}

	[Fact(DisplayName = "JsonLoanRepository.GetLoan: Returns loan when ID is found")]
	public async Task GetLoan_ReturnsLoanWhenIdIsFound()
	{
		var loanId = 1;
		var expectedLoan = new Loan
		{
			Id = loanId,
			BookItemId = 17,
			PatronId = 22,
			LoanDate = DateTime.Parse("2023-12-08T00:40:43.1808862"),
			DueDate = DateTime.Parse("2023-12-22T00:40:43.1808862")
		};
		_mockLoanRepository.GetLoan(loanId).Returns(expectedLoan);

		var expected = await _mockLoanRepository.GetLoan(loanId);
		var actual = await _jsonLoanRepository.GetLoan(loanId);

		Assert.NotNull(expected);
		Assert.NotNull(actual);
		Assert.Equal(expected.Id, actual.Id);
	}
}