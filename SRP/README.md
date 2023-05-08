# Projekt System Rejestracji Pacjêtów poradni psychologicznej.
Projekt mo¿na uruchoimiæ na dwa sposoby:
1. lokalnie na komputerze przy u¿yciu VisualStudio, IIS Expres oraz lokalniej bazy danych MsSql
2. Za pomoc¹ dockera poprzez wywo³anie stworzonego dockerfile oraz docker-compose

AD1.
Uruchamianie programu lokalnie:
Wymagania:
- zainstalowany program visual studio
- zainstalowany sql server i utworzona lokalna serwer.

W przypadku spe³nienia powy¿szych za³o¿eñ nale¿y uruchomiæ program Visual studio i wybraæ opcjê clon repository
W miejscu przeznaczonym na wskazanie lokalizacji repozytorium zdalnego nale¿y wkleiæ poni¿ysz link:
GitHub: https://github.com/Hamau12/Poradnia.git
Kolejnym krokiem jest zmiana formy debugowania z Dockera na IIS Express (Opcja wyboru z listy rozwijanej w górnym menu, obok zielonko trójk¹ta), oraz zmiana ConnectionStrings w tym celu nale¿y uruchomiæ plik appsettings.Development.json i zakomentarzowaæpierwsz¹ linijkê DefaultConnection
oraz odkomentarzowaædrug¹ linijkê DefaultConnection
po wykonaniu powy¿szych czynnoœci mo¿emy uruchomiæ program poprzez klikniecie zielonego trójk¹ta.
Pierwsze uruchomienie bêdzie trwa³o d³u¿ej poniewa¿ program wykona migracjê danych do bazy.
konto testowe
 login:test@pl.pl
 has³o:1qaz@WSX


AD2.
Wymagania:
- pobrany i rozpakowany projekt z Github 
https://github.com/Hamau12/Poradnia/archive/refs/heads/master.zip

- zainstalowany Docker dla kontenerów w œrodowisku linux 

Instalacja na Windows / macOS
Pracuj¹c na macOS b¹dŸ Windowsie w wersji Professional lub Enterprise mo¿emy skorzystaæ z darmowej i bardzo prostej w instalacji wersji – Docker Desktop. 
W przypadku macOS oprogramowanie mo¿emy pobraæ z tej strony:: https://docs.docker.com/desktop/install/mac-install/. i zainstalowaæ korzystaj¹c z gotowej instrukcji: https://docs.docker.com/desktop/install/mac-install/.

Windows-owy plik instalacyjny jest do znalezienia pod tym linkiem: https://docs.docker.com/desktop/install/windows-install/
Po pobraniu instalujemy tak jak ka¿d¹ standardow¹ aplikacjê. Jedna rzecz warta odnotowania – podczas instalacji upewnijmy siê, ¿e opcja 
„Use windows containers instead of Linux containers” nie jest zaznaczona.
Po instalacji restartujemy system (instalator zreszt¹ sam siê o to upomni) i jesteœmy gotowi do pracy. Docker potrzebuje ok. 1-2 minut, aby siê uruchomiæ.
Znajdziemy wtedy w naszym zasobniku now¹ ikonkê.

1. Po spe³nieniu wymagañ nalerzy wejœc do folderu umieszczonego w folderze SRP\SRP, a nastêpnie uruchomiæ ten folder w konsoli terminala. 
2. w konsoli nale¿y uruchomiæ polecenie docker compose up
	-W pierwszym etapie docker zacznie pobieraæ obraz bazy danych która zostanie postawiona na adresie lockalhost,1433 g³ówny u¿ytkownik serwera to: sa, a has³o do niego to 1qaz@WSX
	-W drugim kroku docker utworzy obraz aplikacji 
	-Trzeci krok jest to odpalenie projektu, na pocz¹tku uruchomiona zostanie migracja danych do nowo utworzonej bazy SRP
	(W przypadku, gdy projekt nie uruchomi siê po utworzeniu obrazu nale¿y nacisn¹æ przycisk play usytuowany w DockerDesktop obok obrazu web_api)
	projekt jest dostêpny pod linkiem: http://localhost:5000/
