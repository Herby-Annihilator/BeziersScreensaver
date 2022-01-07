using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeziersScreensaver.Services.Interfaces
{
	public interface INoise<T>
	{
		T GetNoize(T param);
	}
}
