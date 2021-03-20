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

  // Convert back word month format to number like 'Marzec' => '03'
  wordToNumber(month: string) :string{
    let number: string;

    switch(month) {
      case 'Styczeń':
        number = '01';
        break;
      case 'Luty':
        number = '02';
        break;
      case 'Marzec':
        number = '03';
        break;
      case 'Kwiecień':
        number = '04';
        break;
      case 'Maj':
        number = '05';
        break;
      case 'Czerwiec':
        number = '06';
        break;
      case 'Lipiec':
        number = '07';
        break;
      case 'Sierpień':
        number = '08';
        break;
      case 'Wrzesień':
        number = '09';
        break;
      case 'Październik':
        number = '10';
        break;
      case 'Listopad':
        number = '11';
        break;
      case 'Grudzień':
        number = '12';
        break;
    }

    return number;
  }
}
