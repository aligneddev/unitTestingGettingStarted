## Snippets

### Create WeatherControllerTests.WeatherController_GetCurrentTemp_NoZipCode_Returns400

[TestMethod]
public async Task WeatherController_GetCurrentTemp_NoZipCode_Returns400()
{
    /**
     * Given an API call
     * When asking for current temp and no zip code is given
     * Then returns a 400
     **/
}

var controller = Factory();

var result = await controller.CurrentTemp(0);

Assert.Inconclusive();

> in WeatherController
 public Task<ActionResult<double>> CurrentTemp(int zipCode)
{
    return Task.FromResult(52.5);
}

> WeatherControllerTests
Assert.AreEqual(52.5, result);

then to fail
Assert.AreEqual(400, (result.Result as BadRequestObjectResult).StatusCode);

> in WeatherController
 public async Task<ActionResult<double>> CurrentTemp(int zipCode)
{
    if (zipCode == 0)
    {
        return BadRequest($"{nameof(zipCode)} cannot be 0");
    }

    return Ok(10);
}

*** Test passes ***
#### Next Test - WeatherController_GetCurrentTemp_ZipCode_CallsWithZipCode

 [TestMethod]
public async Task WeatherController_GetCurrentTemp_ZipCode_CallsWithZipCode()
{
    /**
        * Given an API call
    * When asking for current temp
    * Then it calls the weather Api with the correct zip code
    */
 
    // Arrange
    var controller = Factory();

    // Act

    // Assert
}

// Arrange
var zipCode = 57105;
var (controller, getWeatherHttpClient) = Factory();

update others with this too

// Assert
Assert.IsTrue(false);

private (WeatherController weatherController, Mock<IGetWeatherHttpClient> getWeatherHttpClient) Factory()
{
    var getWeatherHttpClient = new Mock<IGetWeatherHttpClient>();
    return (new WeatherController(getWeatherHttpClient.Object), getWeatherHttpClient);
}

> Startup.cs needs // already there
services.AddHttpClient<IGetWeatherHttpClient, ApixuClient>();

> Fake the response
// Arrange
var fakeTemp = 72.6;
getWeatherHttpClient.Setup(wp => wp.GetCurrentTempAsync(zipCode)).ReturnsAsync(fakeTemp);


> Test
// Act
var response = await controller.CurrentTemp(zipCode);



// Assert
getWeatherHttpClient.Verify(w => w.GetCurrentTempAsync(zipCode), Times.Once);

> Controller - add call to the client
var result = await weatherHttpClient.GetCurrentTempAsync(zipCode);
return Ok(result);