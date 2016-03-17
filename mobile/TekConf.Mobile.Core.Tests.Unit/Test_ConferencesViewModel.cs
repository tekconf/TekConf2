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
using System;

[TestFixture]
public class Test_ConferencesViewModel : MvxIoCSupportingTest
{
	private IFixture _fixture;
	private IMapper _mapper;

	[SetUp]
	public void SetupTest()
	{
		_fixture = new Fixture();

		var config = new MapperConfiguration(cfg => cfg.CreateMap<ConferenceModel, ConferenceListViewModel>());
		_mapper = config.CreateMapper();

		base.Setup();
	}

	[Test]
	public void should_initialize()
	{
		var conferencesService = new Mock<IConferencesService>();
		var models = _fixture.CreateMany<ConferenceModel>().ToList();
		conferencesService.Setup(x => x.Load()).ReturnsAsync(models);

		var vm = new ConferencesViewModel(conferencesService.Object, _mapper);

		vm.Conferences.ShouldNotBeNull();
		vm.Conferences.Count.ShouldEqual(0);
		vm.IsLoading.ShouldBeFalse();
	}

	[Test]
	public void should_load_conferences_list()
	{
		var conferencesService = new Mock<IConferencesService>();
		var models = _fixture.CreateMany<ConferenceModel>().ToList();
		conferencesService.Setup(x => x.Load()).ReturnsAsync(models);

		var vm = new ConferencesViewModel(conferencesService.Object, _mapper);
		vm.PropertyChanged += (sender, e) =>
		{
			Console.WriteLine("CHANGED");
		};
		vm.LoadCommand.Execute(null);

		vm.Conferences.Count.ShouldEqual(models.Count);
	}

	[Test]
	public void should_set_isLoading()
	{
		var conferencesService = new Mock<IConferencesService>();
		var models = _fixture.CreateMany<ConferenceModel>().ToList();
		conferencesService.Setup(x => x.Load()).Callback(async () => await Task.Delay(1000)).ReturnsAsync(models);

		var vm = new ConferencesViewModel(conferencesService.Object, _mapper);

		vm.LoadCommand.Execute(null);

		vm.IsLoading.ShouldBeTrue();
	}

	[Test]
	public void should_navigate_to_detail()
	{
		true.ShouldBeFalse();
	}

	[Test]
	public void should_refresh()
	{
		true.ShouldBeFalse();
	}
}
