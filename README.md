# Rally Calendar

```
 ______      _ _                          
 | ___ \    | | |                         
 | |_/ /__ _| | |_   _                    
 |    // _` | | | | | |                   
 | |\ \ (_| | | | |_| |                   
 \_| \_\__,_|_|_|\__, |                   
                  __/ |                   
                 |___/                    
            _                _            
           | |              | |           
   ___ __ _| | ___ _ __   __| | __ _ _ __ 
  / __/ _` | |/ _ \ '_ \ / _` |/ _` | '__|
 | (_| (_| | |  __/ | | | (_| | (_| | |   
  \___\__,_|_|\___|_| |_|\__,_|\__,_|_|   
```

Rally Calendar is a console application that allows users to view upcoming rally events. The application provides a user-friendly interface to select different championships and view the corresponding rally events.


## Features

- Choose between different championships (e.g., WRC, ERC).
- Fetch and display upcoming rally events.
- Select events by round.

## Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Installation

1. **Clone the repository:**

    ```bash
    git clone https://github.com/leandropicoli/RallyCalendar.git
    cd RallyCalendar
    ```

2. **Build the project:**

    ```bash
    dotnet build
    ```

3. **Run the application:**

    ```bash
    dotnet run --project RallyCalendar.Console
    ```

## Usage

When you run the application, you will see an ASCII art splash screen, followed by options to choose a championship. After selecting a championship, the application will fetch and display the upcoming events. You can then choose an event by its round number to see more details.

### Example

```plaintext
Rally Calendar

Choose the Championship:
1 - WRC
2 - ERC
1
You have chosen WRC

Fetching WRC events. Please wait

1 - [wrc] Monaco - Rallye Monte-Carlo / 2024, 24-01 - 28-01
2 - [wrc] Sweden - WRC Rally Sweden / 2024, 15-02 - 18-02
...
Choose an event by Round:
1

You have chosen: Rallye Monte-Carlo
ICS file created successfully!
