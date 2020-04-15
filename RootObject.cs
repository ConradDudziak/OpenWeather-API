// Conrad Dudziak -- CSS436 Autumn 2019
// Program 2 - REST
// This file is an Object that gets instantiated and populated
// by a JSON converter. This is a client app which consumes the RESTful OpenWeather API.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherREST {
	// The RootObject that gets instantiated by the JSON converter.
	public class RootObject {
		public Coord coord { get; set; }
		public Weather[] weather { get; set; }
		public string _base { get; set; }
		public Main main { get; set; }
		public int visibility { get; set; }
		public Wind wind { get; set; }
		public Clouds clouds { get; set; }
		public int dt { get; set; }
		public Sys sys { get; set; }
		public int id { get; set; }
		public string name { get; set; }
		public int cod { get; set; }
	}
	
	// A class that contains coordinate information for RootObject.
	public class Coord {
		public float lon { get; set; }
		public float lat { get; set; }
	}

	// A class that contains temperature, pressure, and humidity
	// information for RootObject.
	public class Main {
		public float temp { get; set; }
		public int pressure { get; set; }
		public int humidity { get; set; }
		public float temp_min { get; set; }
		public float temp_max { get; set; }
	}

	// A class that contains wind information for RootObject.
	public class Wind {
		public float speed { get; set; }
		public float deg { get; set; }
	}

	// A class that contains cloud information for RootObject.
	public class Clouds {
		public int all { get; set; }
	}

	// A class that contains country, sun, and id information for RootObject.
	public class Sys {
		public int type { get; set; }
		public int id { get; set; }
		public float message { get; set; }
		public string country { get; set; }
		public int sunrise { get; set; }
		public int sunset { get; set; }
	}

	// A class that contains a description of the weather and some metadata.
	public class Weather {
		public int id { get; set; }
		public string main { get; set; }
		public string description { get; set; }
		public string icon { get; set; }
	}
}
