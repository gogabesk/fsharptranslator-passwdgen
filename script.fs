open System
open System.Security.Cryptography
let tes = "Иванов Иван"
let uppercase (tes : string) = tes.ToUpper()
let tes1 = tes |> uppercase
let ret = new System.Text.StringBuilder()
let rus = [|"А"; "Б"; "В"; "Г"; "Д"; "Е"; "Ё"; "Ж"; "З"; "И"; "Й"; "К"; "Л"; "М"; "Н"; "О"; "П"; "Р"; "С"; "Т"; "У"; "Ф"; "Х"; "Ц"; "Ч"; "Ш"; "Щ"; "Ъ"; "Ы"; "Ь"; "Э"; "Ю"; "Я"; " "|]
let eng = [|"a"; "b"; "v"; "g"; "d"; "e"; "e"; "j"; "z"; "i"; "y"; "k"; "l"; "m"; "n"; "o"; "p"; "r"; "s"; "t"; "u"; "f"; "h"; "c"; "ch"; "h"; "sh"; "9"; "y"; "6"; "e"; "yu"; "ya"; "."|]
let genlogin  =
 for j = 0 to tes1.Length-1 do
  for i = 0 to rus.Length-1 do
   let test1 = tes1.Substring(j, 1).Contains rus.[i]
   if test1 = true then ret.Append(eng.[i])|> ignore
 ret.ToString()

let results =
 if (genlogin.Length > 10) then genlogin.[0..10].ToString()
 else genlogin

printfn "%s" results

let charSet = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
let charSetLength = byte charSet.Length

let randomByte (rng:RandomNumberGenerator) =                        
    let roll = Array.zeroCreate 1
    rng.GetBytes(roll)
    roll.[0]

let isFairRoll (numSides:byte) (roll:byte) =                        
    let fullSetsOfValues = Byte.MaxValue / numSides                 
    roll < numSides * fullSetsOfValues                              

let rec rollDie (rng:RandomNumberGenerator) (numSides:byte) =      
    let randomByte = randomByte(rng)                                
    match isFairRoll numSides randomByte with 
    |false -> rollDie rng numSides                                  
    |true -> (randomByte % numSides)

let charFromCharSet (charSet:string) position =                     
    charSet.Substring(position,1)
      
let generatePassword passwordLength =                                
    use rng:RandomNumberGenerator = RandomNumberGenerator.Create()  
    let sb = new System.Text.StringBuilder()
    for i in 1 .. 10 do
        let position = int (rollDie rng charSetLength)
        sb.Append(charFromCharSet charSet position) |> ignore
    sb.ToString()
     
let passwd = generatePassword ""

printfn "%s" passwd                       
