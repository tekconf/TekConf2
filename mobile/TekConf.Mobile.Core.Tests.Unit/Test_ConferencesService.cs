using NUnit.Framework;
using Ploeh.AutoFixture;
using Moq;
using Should;
using TekConf.Mobile.Core.ViewModels;
using MvvmCross.Test.Core;
using System.Linq;
using AutoMapper;
using TekConf.Mobile.Core;
using System.Threading;
using System.Threading.Tasks;

[TestFixture]
public class Test_ConferencesService : MvxIoCSupportingTest
{
	private IFixture _fixture;
	private IMapper _mapper;

	[SetUp]
	public void SetupTest()
	{
		_fixture = new Fixture();

		base.Setup();
	}

	[Test]
	public void should_load_from_local_database()
	{
		true.ShouldBeFalse();
	}

	[Test]
	public void should_load_from_remote_service()
	{
		true.ShouldBeFalse();
	}
}
