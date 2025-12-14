namespace LibrarySystem

open System
open System.Text.RegularExpressions

// 1. UNIQUE IDENTIFIERS (Type Safety Wrappers)
type BookId = BookId of Guid
type MemberId = MemberId of string

// 2. DOMAIN ERRORS (Structured Errors)
type LibraryError =
    | BookNotFound
    | BookNotAvailable
    | InvalidData of string
    | StorageError of string

// 3. VALIDATED PRIMITIVE (Smart Constructor)
module ISBN =
    type ISBN = private ISBN of string

    // Smart constructor to validate ISBN
    let create (s: string) =
        if String.IsNullOrWhiteSpace(s) then Error "ISBN cannot be empty"
        else Ok (ISBN s)

    // Accessor function to safely unwrap the value
    let value (ISBN s) = s

// 4. STATE MANAGEMENT (Discriminated Union)
type BookStatus =
    | Available
    | Borrowed of memberId: MemberId * dueDate: DateTime

// 5. THE CORE ENTITY (Record Type)
type Book = {
    Id: BookId
    Title: string
    Author: string
    ISBN: ISBN
    Status: BookStatus
}