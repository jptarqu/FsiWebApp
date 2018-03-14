// Step 0. Boilerplate to get the paket.exe tool
 
open System
open System.IO
 
Environment.CurrentDirectory <- __SOURCE_DIRECTORY__


// ///////////// FSI
// open System.Text
// open Microsoft.FSharp.Compiler.SourceCodeServices
// open Microsoft.FSharp.Compiler.Interactive.Shell

// // Intialize output and input streams
// let sbOut = new StringBuilder()
// let sbErr = new StringBuilder()
// let inStream = new StringReader("")
// let outStream = new StringWriter(sbOut)
// let errStream = new StringWriter(sbErr)

// // Build command line arguments & start FSI session
// let argv = [| "fsi.exe" |]
// let allArgs = Array.append argv [|"--noninteractive"|]

// let fsiConfig = FsiEvaluationSession.GetDefaultConfiguration()
// let fsiSession = FsiEvaluationSession.Create(fsiConfig, allArgs, inStream, outStream, errStream) 
// let evalExpression text =
//   match fsiSession.EvalExpression(text) with
//   | Some value -> printfn "%A" value.ReflectionValue
//   | None -> printfn "Got no result!"

// Step 2. Use the packages
 
#r "packages/Suave/lib/net40/Suave.dll"
 
open Suave // always open suave
open System.Threading

module myTest =
    let sayHi() = 
        // let code = File.ReadAllText("dynamicFsharp.fs")
        // printfn "%s" code
        // let res = evalExpression code
        // res.ToString()
        "Hola!"

let Hi() = (myTest.sayHi())


let mutable cts: CancellationTokenSource = null

let StartSuave() = 
    if isNull cts then () else cts.Cancel()
    cts <- new CancellationTokenSource()
    let conf = { defaultConfig with cancellationToken = cts.Token }
    
    let mainHandler = request (fun r -> 
        printfn "%s" (myTest.sayHi())
        Successful.OK (myTest.sayHi())
        )
    let listening, server = startWebServerAsync conf mainHandler

    Async.Start(server, cts.Token)
    printfn "Make requests now"

// NOtes from experiemnt:

//  you must :
//  1. send the slection of your business logic that chnaged to FSI
//  2. resend the body of StartSuave() (the server bootstrapper)
//  3. call StartSuave() to restart the suave server
// TODO: separate business myTest module into separate file, on file saved re-send that file and then re-send the bootstrapper and call the start?