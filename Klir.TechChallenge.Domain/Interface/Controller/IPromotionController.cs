using Klir.TechChallenge.Domain.EntityModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Klir.TechChallenge.Domain.Interface.Controller
{
    public interface IPromotionController
    {
        IEnumerable<Promotion> Get();
    }
}
