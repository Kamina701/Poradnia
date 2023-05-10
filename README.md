# Projekt System Rejestracji Pacjentów Poradni Psychologicznej.
Projekt można uruchoimić na dwa sposoby:
1. lokalnie na komputerze przy użyciu VisualStudio, IIS Expres oraz lokalnej bazy danych MsSql
2. Za pomocą dockera poprzez wywołanie stworzonego dockerfile oraz docker-compose

AD1.
Uruchamianie programu lokalnie:
Wymagania:
- zainstalowany program visual studio
- zainstalowany sql server i utworzony lokalny serwer.

W przypadku spełnienia powyższych założeń należy uruchomić program Visual studio i wybrać opcję clon repository
W miejscu przeznaczonym na wskazanie lokalizacji repozytorium zdalnego należy wkleić poniższy link:
GitHub: https://github.com/Hamau12/Poradnia.git
Kolejnym krokiem jest zmiana formy debugowania z Dockera na IIS Express (Opcja wyboru z listy rozwijanej w górnym menu, obok zielonego trójkąta), oraz zmiana ConnectionStrings w tym celu należy uruchomić plik appsettings.Development.json i zakomentarzować pierwszą linijkę DefaultConnection
oraz odkomentarzować drugą linijkę DefaultConnection
po wykonaniu powyższych czynności możemy uruchomić program poprzez klikniecie zielonego trójkąta.
Pierwsze uruchomienie będzie trwało dłużej ponieważ program wykona migrację danych do bazy.
konto testowe
 login:test@pl.pl
 hasło:1qaz@WSX


AD2.
Wymagania:
- pobrany i rozpakowany projekt z Github 
https://github.com/Hamau12/Poradnia/archive/refs/heads/master.zip

- zainstalowany Docker dla kontenerów w środowisku linux 

Instalacja na Windows / macOS
Pracując na macOS bądź Windowsie w wersji Professional lub Enterprise możemy skorzystać z darmowej i bardzo prostej w instalacji wersji – Docker Desktop. 
W przypadku macOS oprogramowanie możemy pobrać z tej strony:: https://docs.docker.com/desktop/install/mac-install/. i zainstalować korzystając z gotowej instrukcji: https://docs.docker.com/desktop/install/mac-install/.

Windows-owy plik instalacyjny jest do znalezienia pod tym linkiem: https://docs.docker.com/desktop/install/windows-install/
Po pobraniu instalujemy tak jak każdą standardową aplikację. Jedna rzecz warta odnotowania – podczas instalacji upewnijmy się, że opcja 
„Use windows containers instead of Linux containers” nie jest zaznaczona.
Po instalacji restartujemy system (instalator zresztą sam się o to upomni) i jesteśmy gotowi do pracy. Docker potrzebuje ok. 1-2 minut, aby się uruchomić.
Znajdziemy wtedy w naszym zasobniku nową ikonkę.

1. Po spełnieniu wymagań należy wejśc do folderu umieszczonego w folderze SRP\SRP, a następnie uruchomić ten folder w konsoli terminala. 
2. w konsoli należy uruchomić polecenie docker compose up
	-W pierwszym etapie docker zacznie pobierać obraz bazy danych która zostanie postawiona na adresie lockalhost,1433 główny użytkownik serwera to: sa, a hasło do niego to 1qaz@WSX
	-W drugim kroku docker utworzy obraz aplikacji 
	-Trzeci krok jest to odpalenie projektu, na początku uruchomiona zostanie migracja danych do nowo utworzonej bazy SRP
	(W przypadku, gdy projekt nie uruchomi się po utworzeniu obrazu należy nacisnąć przycisk play usytuowany w DockerDesktop obok obrazu web_api)
	projekt jest dostępny pod linkiem: http://localhost:5000/
