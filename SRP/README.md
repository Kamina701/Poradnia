# Projekt System Rejestracji Pacj�t�w poradni psychologicznej.
Projekt mo�na uruchoimi� na dwa sposoby:
1. lokalnie na komputerze przy u�yciu VisualStudio, IIS Expres oraz lokalniej bazy danych MsSql
2. Za pomoc� dockera poprzez wywo�anie stworzonego dockerfile oraz docker-compose

AD1.
Uruchamianie programu lokalnie:
Wymagania:
- zainstalowany program visual studio
- zainstalowany sql server i utworzona lokalna serwer.

W przypadku spe�nienia powy�szych za�o�e� nale�y uruchomi� program Visual studio i wybra� opcj� clon repository
W miejscu przeznaczonym na wskazanie lokalizacji repozytorium zdalnego nale�y wklei� poni�ysz link:
GitHub: https://github.com/Hamau12/Poradnia.git
Kolejnym krokiem jest zmiana formy debugowania z Dockera na IIS Express (Opcja wyboru z listy rozwijanej w g�rnym menu, obok zielonko tr�jk�ta), oraz zmiana ConnectionStrings w tym celu nale�y uruchomi� plik appsettings.Development.json i zakomentarzowa�pierwsz� linijk� DefaultConnection
oraz odkomentarzowa�drug� linijk� DefaultConnection
po wykonaniu powy�szych czynno�ci mo�emy uruchomi� program poprzez klikniecie zielonego tr�jk�ta.
Pierwsze uruchomienie b�dzie trwa�o d�u�ej poniewa� program wykona migracj� danych do bazy.
konto testowe
 login:test@pl.pl
 has�o:1qaz@WSX


AD2.
Wymagania:
- pobrany i rozpakowany projekt z Github 
https://github.com/Hamau12/Poradnia/archive/refs/heads/master.zip

- zainstalowany Docker dla kontener�w w �rodowisku linux 

Instalacja na Windows / macOS
Pracuj�c na macOS b�d� Windowsie w wersji Professional lub Enterprise mo�emy skorzysta� z darmowej i bardzo prostej w instalacji wersji � Docker Desktop. 
W przypadku macOS oprogramowanie mo�emy pobra� z tej strony:: https://docs.docker.com/desktop/install/mac-install/. i zainstalowa� korzystaj�c z gotowej instrukcji: https://docs.docker.com/desktop/install/mac-install/.

Windows-owy plik instalacyjny jest do znalezienia pod tym linkiem: https://docs.docker.com/desktop/install/windows-install/
Po pobraniu instalujemy tak jak ka�d� standardow� aplikacj�. Jedna rzecz warta odnotowania � podczas instalacji upewnijmy si�, �e opcja 
�Use windows containers instead of Linux containers� nie jest zaznaczona.
Po instalacji restartujemy system (instalator zreszt� sam si� o to upomni) i jeste�my gotowi do pracy. Docker potrzebuje ok. 1-2 minut, aby si� uruchomi�.
Znajdziemy wtedy w naszym zasobniku now� ikonk�.

1. Po spe�nieniu wymaga� nalerzy wej�c do folderu umieszczonego w folderze SRP\SRP, a nast�pnie uruchomi� ten folder w konsoli terminala. 
2. w konsoli nale�y uruchomi� polecenie docker compose up
	-W pierwszym etapie docker zacznie pobiera� obraz bazy danych kt�ra zostanie postawiona na adresie lockalhost,1433 g��wny u�ytkownik serwera to: sa, a has�o do niego to 1qaz@WSX
	-W drugim kroku docker utworzy obraz aplikacji 
	-Trzeci krok jest to odpalenie projektu, na pocz�tku uruchomiona zostanie migracja danych do nowo utworzonej bazy SRP
	(W przypadku, gdy projekt nie uruchomi si� po utworzeniu obrazu nale�y nacisn�� przycisk play usytuowany w DockerDesktop obok obrazu web_api)
	projekt jest dost�pny pod linkiem: http://localhost:5000/
