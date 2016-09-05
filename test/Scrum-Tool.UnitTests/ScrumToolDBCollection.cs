using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ASPNET5_Scrum_Tool.Models;
using Xunit;

namespace Scrum_Tool.UnitTests
{
	[CollectionDefinition("ScrumToolDB Collection")]
	public class ScrumToolDBCollection : ICollectionFixture<ScrumToolDBFixture>
	{
		// This class has no code, and is never created. Its purpose is simply
    	// to be the place to apply [CollectionDefinition] and all the
    	// ICollectionFixture<> interfaces.
	}

}
