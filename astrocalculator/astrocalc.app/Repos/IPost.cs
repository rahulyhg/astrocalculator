using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace astrocalc.app.repos {
    public interface IPost<T> {
        Task<T> Create(T toCreate);
    }
}
