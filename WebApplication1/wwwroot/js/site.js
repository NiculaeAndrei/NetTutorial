const uri = 'api/Cifs';



function addItem() {
    const addNameTextbox = document.getElementById('add-cif');
    const tableElement = document.getElementsByClassName('cif-table');

  const item = {
    isValid: false,
    name: addNameTextbox.value.trim()
  };

  fetch(uri, {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(item)
  })
      .then(response => {
          if (response.ok) {
              return response.json();
          }

          return null;
      })
    .then(cif => {
        addNameTextbox.value = '';

        if (!cif) {
            return;
        }

        _displayItems(cif);
    })
    .catch(error => console.error('Unable to add item.', error));
}







function _displayItems(data) {
    if (!data) {
        return;
    }

  const tBody = document.getElementById('todos');

  

  const button = document.createElement('button');

    let isCompleteCheckbox = document.createElement('input');
    isCompleteCheckbox.type = 'checkbox';
    isCompleteCheckbox.disabled = true;
    isCompleteCheckbox.checked = data.isValid;

   

    let tr = tBody.insertRow();
    
    let td1 = tr.insertCell(0);
    let textNode = document.createTextNode(data.isValid);
    td1.appendChild(textNode);

    let td2 = tr.insertCell(1);
    textNode = document.createTextNode(data.name);
    td2.appendChild(textNode);

    let td3 = tr.insertCell(2);
    textNode = document.createTextNode(data.denumire);
    td3.appendChild(textNode);

    let td4 = tr.insertCell(3);
    textNode = document.createTextNode(data.adresa);
    td4.appendChild(textNode);


    let td5 = tr.insertCell(4);
    textNode = document.createTextNode(data.platitorTVA);
    td5.appendChild(textNode);

    let td6 = tr.insertCell(5);
    textNode = document.createTextNode(data.statusTVAIncasare);
    td6.appendChild(textNode);
    
    let td7 = tr.insertCell(6);
    textNode = document.createTextNode(data.statusActiv);
    td7.appendChild(textNode);

  
}