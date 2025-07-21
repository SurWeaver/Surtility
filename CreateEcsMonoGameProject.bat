@REM Первый аргумент - название проекта

@echo off

@REM Выход из скрипта, если забыл название проекта
if [%1]==[] goto usage

@REM Создание папки с названием проекта
mkdir %1
cd .\%1\

@REM Создание решения, игрового проекта и библиотеки
dotnet new sln -n %1
dotnet new classlib -o %1.Core
dotnet new mgdesktopgl -o %1.Main

git init
git submodule add https://github.com/SurWeaver/Surtility.git
git submodule update --init --recursive

@REM Замена старой версии языка в игровом проекте на совместимый с библиотекой (с 8 на 9)
powershell -Command "(Get-Content '%1.Main\%1.Main.csproj') -replace '<TargetFramework>net[0-9.]+</TargetFramework>', '<TargetFramework>net9.0</TargetFramework>' | Set-Content '%1.Main\%1.Main.csproj'"

@REM Замена nullable enable на disable
powershell -Command "(Get-Content '%1.Main\%1.Main.csproj') -replace '<Nullable>enable</Nullable>', '<Nullable>disable</Nullable>' | Set-Content '%1.Main\%1.Main.csproj'"

@REM Связка проектов с решением
dotnet sln add %1.Core/%1.Core.csproj
dotnet sln add %1.Main/%1.Main.csproj
dotnet sln add Surtility\Surtility\Surtility.csproj

@REM Настройка Core-проекта с ECS и моими инструментами
cd %1.Core
mkdir ECS\Components,ECS\Systems,Utils
dotnet add reference ..\Surtility\Surtility\Surtility.csproj

@REM Настройка игрового проекта
cd ..\%1.Main\
dotnet add reference ..\%1.Core\%1.Core.csproj
mkdir Content\Textures,Content\Fonts,Content\Sounds

cd ..
dotnet build
ECHO Project %1 successfully created!

@REM Включение VS Code
code .

goto :eof

@REM Сообщение о правильном использовании при отсутствии имени проекта
:usage
@echo Right usage is: %0 ^<ProjectName^>
exit /B 1
