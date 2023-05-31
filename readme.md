# Snap Telegram Bot

A simple telegram bot to take a picture from a connected web cam.

The bot uses big `Emgu.CV` lib to work with the web cam, let me know if you know better way.

## Setup Instructions
1. Obtain a telegram bot auth token.
   1. Create a telegram bot using [@BotFather](https://t.me/botfather).
2. Compile the project.
3. Configure your bot.
   1. Adjust `appsettings.json` file with auth token.
   2. Add your telegram user id to Security -> Allowed Users array.
      1. You can send `/start` command to the bot to get your user id.
4. Run your bot, send `/takepicture` to take a picture.

## Building
Publish single file: `dotnet publish -r win-x64 -p:PublishSingleFile=true --self-contained true`