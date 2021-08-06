# JurassicPark

## Transfer Dino

- First search through Dino database for findName
  - If Dino database.Name(findName) == null
    - return no Dino by that name in our database
  - else
    - If Dino database.Name(findName).Contains > 1
      - print list of Dinos database.Name(findName)
      - print "Which dino would you like to move? "
      - database.DinoType(findName)
      - Console.readline()
      - print "Where would you like to move database.Name()?"
      - Console.readline()
      - print "database.Name has been moved to Enclosure #"
    - else
      - print "Where would you like to move database.Name()?"
      - Console.readline()
      - print "database.Name has been moved to Enclosure #"
