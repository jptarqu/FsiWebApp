namespace WebFileManager

open System.IO

type FsiSender = string->unit

type FileProcessor(projFolderPath: string, startFilePath: string, fsiSender: FsiSender) = 
    let a = 9
    let firstFileContents =  File.ReadAllText(startFilePath)
    
    do 
        fsiSender "1 + 1" //firstFileContents
    

    member this.DoWatch() = ()