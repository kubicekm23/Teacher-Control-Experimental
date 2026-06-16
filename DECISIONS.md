# Design Decisions - Teacher Control Features

This document outlines the logic and assumptions made during the implementation of features where the original requirements were underspecified.

## 1. Bingo (Král/královna dne)
- **Scope:** The Bingo board is **Global** and **Daily**. All users interact with the same board for the current day.
- **Content:** The board is a 5x5 grid (25 tiles). Tiles are randomly selected from a predefined pool of "Teacher Events" (e.g., "Učitel zapomněl klíče", "Učitel přišel včas", "Učitel vypráví historku").
- **Win Condition:** The first user to complete a row, column, or diagonal (5 tiles in a line) triggers the "Win".
- **Reward:** The winner's username is displayed on the Bingo page as "Dnešním vítězem je: [Uživatel]".
- **Fairness:** Tiles are triggered by users. A "Trigger" marks the tile as "happened". The first person to trigger the *last* tile in a winning line becomes the winner.

## 2. Meme Management
- **Selection:** Only memes uploaded or linked by **Admins** can be used in the chat. This prevents unmoderated image sharing while allowing for "School Memes" as requested.
- **Usage:** In the Chat interface, users see a "Meme" button (image icon) which opens a modal with a gallery of approved memes.
- **Moderation:** Admins have a dedicated "Memes" section in the Admin Panel to Add, Edit, or Delete memes.

## 3. Overall Rating Formula
- **Concept:** A teacher's "Experimental Score" (1-5) that dynamically updates based on multiple factors.
- **Formula:**
  `FinalScore = AverageStars - (TotalLatenessMinutes / 60 * 0.2) + (TotalVotes / 10 * 0.1)`
- **Constraints:** The result is clamped between 1.0 and 5.0.
- **Rationale:**
    - Reviews are the primary metric.
    - Lateness is a penalty (each hour late loses 0.2 points).
    - Positive votes are a bonus (each 10 votes adds 0.1 points).
    - All votes (e.g., "Nejlepší hlášky", "Nejvíc sexy") are currently treated as positive engagement.

## 4. Technical Architecture
- **BingoService:** Encapsulates board generation and win checking. Registered as Scoped.
- **TeacherRatingService:** Encapsulates the experimental score calculation. Registered as Scoped.
- **Daily Reset:** The `BingoBoard` is automatically created on the first request of each day (UTC).
