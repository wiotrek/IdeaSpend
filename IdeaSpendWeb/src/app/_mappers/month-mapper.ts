export class MonthMapper {

  constructor(){}

  // Convert number month format to word like '03' => 'Marzec'
  numberToWord(number: string) :string{
    let monthWord: string;

    switch(number) {
      case '1':
        monthWord = 'Styczeń';
        break;
      case '2':
        monthWord = 'Luty';
        break;
      case '3':
        monthWord = 'Marzec';
        break;
      case '4':
        monthWord = 'Kwiecień';
        break;
      case '5':
        monthWord = 'Maj';
        break;
      case '6':
        monthWord = 'Czerwiec';
        break;
      case '7':
        monthWord = 'Lipiec';
        break;
      case '8':
        monthWord = 'Sierpień';
        break;
      case '9':
        monthWord = 'Wrzesień';
        break;
      case '10':
        monthWord = 'Październik';
        break;
      case '11':
        monthWord = 'Listopad';
        break;
      case '12':
        monthWord = 'Grudzień';
        break;
    }

    return monthWord;
  }
}
