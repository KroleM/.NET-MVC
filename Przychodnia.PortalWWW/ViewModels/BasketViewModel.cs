﻿using Przychodnia.Database.Data.Visits;

namespace Przychodnia.PortalWWW.ViewModels
{
	public class BasketViewModel
	{
		public List<BasketElement> BasketElements { get; set; }
		public decimal Razem { get; set; }
	}
}