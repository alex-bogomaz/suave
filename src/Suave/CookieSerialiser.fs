namespace Suave

open System.IO

type CookieSerialiser =
  abstract serialise : Map<string, obj> -> byte []
  abstract deserialise : byte [] -> Map<string, obj>


open System.Runtime.Serialization.Json

(*type BinaryFormatterSerialiser() =
  interface CookieSerialiser with
    member x.serialise m =
      let output = Newtonsoft.Json.JsonConvert.SerializeObject(m)
      System.Text.Encoding.Unicode.GetBytes(output)
      
    member x.deserialise data =
      let json = System.Text.Encoding.Unicode.GetString(data)
      Newtonsoft.Json.JsonConvert.DeserializeObject<_>(json)*)


open System.Runtime.Serialization.Formatters.Binary

type BinaryFormatterSerialiser() =
  interface CookieSerialiser with
    member x.serialise m =
      use ms = new MemoryStream()
      let f = new BinaryFormatter()
      f.Serialize(ms, m)
      ms.ToArray()

    member x.deserialise data =
      use ms = new MemoryStream(data)
      let f = new BinaryFormatter()
      f.Deserialize ms :?> _
