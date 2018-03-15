namespace WebFileManager

open System
open System.IO
open System.Text
open Microsoft.FSharp.Compiler.SourceCodeServices
open Microsoft.FSharp.Compiler.Interactive.Shell

type FsiHelper() =
    // Intialize output and input streams
    let sbOut = new StringBuilder()
    let sbErr = new StringBuilder()
    let inStream = new StringReader("")
    let outStream = new StringWriter(sbOut)
    let errStream = new StringWriter(sbErr)

    // Build command line arguments & start FSI session
    let argv = [| "c:\\fsi.exe" |]
    let allArgs = Array.append argv [|"--noninteractive"|]

    let fsiConfig = FsiEvaluationSession.GetDefaultConfiguration()
    let fsiSession = FsiEvaluationSession.Create(fsiConfig, allArgs, inStream, Console.Out, Console.Error)

    member this.SendToFsi(code: string) =
        fsiSession.EvalInteraction(code)
        let result, warnings = fsiSession.EvalInteractionNonThrowing code

        // show the result
        match result with 
        | Choice1Of2 () -> printfn "checked and executed ok %s" (sbOut.ToString())
        | Choice2Of2 exn -> printfn "execution exception: %s" exn.Message
