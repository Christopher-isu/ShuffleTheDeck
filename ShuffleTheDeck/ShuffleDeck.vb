'ChristopherZ
'Spring 2025
'RCET2265
'Shuffle The Deck
'

Option Explicit On 'forces all variables to be declared
Option Strict On 'forces all data types to match
Module ShuffleDeck
    Dim deck(3, 12) As Boolean ' Boolean array to track if a card has already been dealt
    Dim suits As String() = {"Spades", "Clubs", "Hearts", "Diamonds"}
    Dim values As String() = {"2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"}
    Dim rnd As New System.Random()

    Sub Main()
        ShuffleDeck()
        System.Console.WriteLine("Press any key to draw a card, 'S' to shuffle the deck, or 'Q' to quit.")

        Do
            Dim input As String = System.Console.ReadLine()
            If input.ToUpper() = "S" Then
                ShuffleDeck()
                System.Console.WriteLine("Deck shuffled. Press any key to draw a card, 'S' to shuffle the deck, or 'Q' to quit.")
            ElseIf input.ToUpper() = "Q" Then
                Exit Do ' Quit the program
            Else
                DrawCard()
                DisplayDeck()
            End If
        Loop While True

        System.Console.WriteLine("Thank you for playing! Goodbye!")
    End Sub

    Sub ShuffleDeck()
        For i As Integer = 0 To 3
            For j As Integer = 0 To 12
                deck(i, j) = False ' Reset the deck
            Next
        Next
    End Sub

    Sub DrawCard()
        Dim suitIndex, valueIndex As Integer

        Do
            suitIndex = rnd.Next(0, 4)
            valueIndex = rnd.Next(0, 13)
        Loop While deck(suitIndex, valueIndex) ' Keep generating until an undealt card is found

        deck(suitIndex, valueIndex) = True ' Mark the card as dealt
        System.Console.WriteLine("Card drawn: " & values(valueIndex) & " of " & suits(suitIndex))

        ' Check if all cards have been dealt
        If IsDeckEmpty() Then
            System.Console.WriteLine("All cards have been dealt. Shuffling the deck.")
            ShuffleDeck()
        End If
    End Sub

    Sub DisplayDeck()
        System.Console.WriteLine("|       | Spades  |  Clubs  | Hearts  | Diamonds|")
        System.Console.WriteLine("|-------|---------|---------|---------|---------|")

        For valueIndex As Integer = 0 To 12
            Dim row As String = "| " & values(valueIndex).PadRight(5) & " |"
            For suitIndex As Integer = 0 To 3
                If deck(suitIndex, valueIndex) Then
                    row &= "    █    |" ' Mark dealt card with an 'X'
                Else
                    row &= "         |"
                End If
            Next
            System.Console.WriteLine(row)
        Next
    End Sub

    Function IsDeckEmpty() As Boolean
        For i As Integer = 0 To 3
            For j As Integer = 0 To 12
                If Not deck(i, j) Then
                    Return False
                End If
            Next
        Next
        Return True
    End Function
End Module
