using System;
using Xunit;
using CarData.Controllers;
using CarData.Models;

namespace CarData.Tests
{
   
public class CarDetailsControllertests
{

	private CarDetailsController _carDetailsController;
	public CarDetailsControllertests()
	{
			_carDetailsController = new CarDetailsController();
	}


		[Fact]
        public void GetCarDetails_returnsCarDetails()
        {
            var result = _carDetailsController.GetCarDetails();
            var mock = new CarDetails(){
                Name= Sedan;
                Price = 300000.00;
            };
            System.Console.WriteLine(result);
            Assert.That(mock.Count , Is.GreaterThan(0));
        }

    	[Fact]
        public void PostCarDetails_returnsAddedEntity()
        {
            var result = _carDetailsController.GetCarDetails();
            var mock = new CarDetails(){
                Name= Sedan;
                Price = 300000.00;
            };
            System.Console.WriteLine(result);
            Assert.IsNotNull(mock);
        }

}
}