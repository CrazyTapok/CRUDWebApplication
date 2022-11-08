const prefix = 'https://localhost:44367/api/Contacts';

const table = document.querySelector('tbody')
const myForm = document.forms.form

const loader = document.querySelector('.loader_box')

const updateListBtn = document.querySelector('#updateList')
const addUserBtn = document.querySelector('#addUser')

const modalTitle = document.querySelector('#modal-title')
const modalWindow = document.querySelector('.modal')



getAllUsers()


updateListBtn.addEventListener('click', event => {
    event.preventDefault()
    getAllUsers()
})

addUserBtn.addEventListener('click', event => {
    event.preventDefault();
    modalTitle.innerHTML = 'Add new contact'
    modalWindow.hidden = false
})


document.querySelector('.close').addEventListener('click', () => {
    modalWindow.hidden = true
    cleaningForm()
    document.forms.form.reset()
})

window.onclick = function (event) {
    if (event.target === modalWindow) {
        modalWindow.hidden = true
        cleaningForm()
        document.forms.form.reset()
    }
}

myForm.addEventListener('reset', () => {
    cleaningForm()
    modalTitle.innerHTML = 'Add new contact'
})


myForm.addEventListener('submit', event => {
    event.preventDefault()
    cleaningForm()

    let id = myForm.id.value
    let name = myForm.name.value
    let phone = myForm.phone.value
    let job = myForm.job.value
    let birthDate = myForm.birthDate.value


    if (/^[а-яa-z ]*$/i.test(name) && name.trim() !== "" && /[^а-яа-яa-za-z]$/.test(phone) && job.trim() !== "" && birthDate.trim() !== "") {
        if (id.trim() !== "") {
            editContact(id, { name, phone, job, birthDate })
        } else {
            addContact({ name, phone, job, birthDate })
        }

        modalWindow.hidden = true
        document.forms.form.reset()
    } else {

        if (!(/^[а-яa-z]*$/i.test(name))) {
            addInvalid(myForm.name, 'Invalid characters in field!')
        }

        if (name.trim() == "") {
            addInvalid(myForm.name, 'Field is required!')
        }

        if (!(/[^а-яа-яa-za-z]$/.test(phone))) {
            addInvalid(myForm.phone, 'Wrong phone number entered!')
        }

        if (job.trim() == "") {
            addInvalid(myForm.job, 'Field is required!')
        }

        if (birthDate.trim() == "") {
            addInvalid(myForm.birthDate, 'Field is required!')
        }
    }
})


function getAllUsers() {
    loader.hidden = false

    fetch(prefix, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json; charset=utf-8'
        }
        
    })
    .then(response => response.json())
    .then(result => {
        table.hidden = true
        table.innerHTML = ""
        result.forEach(user => {
            table.append(row(user))
        })
    })
    .then(() => {
            setTimeout(() => {
            loader.hidden = true
            table.hidden = false
        }, "600")
    })
    .catch(error => console.error('Unable to get contacts', error))
}


function row(User) {
    let tr = document.createElement('tr')

    function addCell(user, row, name) {
        let cell = document.createElement('td')

        if (name === 'table-btn') {

            let editBtn = document.createElement('button')
            editBtn.classList.add('editBtn')
            editBtn.innerHTML = '<ion-icon name="create-outline"></ion-icon>'

            editBtn.addEventListener("click", event => {
                event.preventDefault()

                getContact(User.id)
                modalTitle.innerHTML = 'Update information'
            })
            cell.append(editBtn)


            let deleteBtn = document.createElement('button')
            deleteBtn.classList.add("deleteBtn")
            deleteBtn.innerHTML = '<ion-icon name="trash-outline"></ion-icon>'

            deleteBtn.addEventListener('click', event => {
                event.preventDefault();

                deleteUser(User.id)
            })
            cell.append(deleteBtn)

        }
        else if (name == 'birthDate') {

            cell.innerHTML = getShortDate(user[name], true)

        } else {

            cell.innerHTML = user[name]
        }
        row.append(cell)
    }

    addCell(User, tr, 'name')
    addCell(User, tr, 'mobilePhone')
    addCell(User, tr, 'jobTitle')
    addCell(User, tr, 'birthDate')
    addCell(User, tr, 'table-btn')

    return tr;
}


function getContact(id) {
    fetch(`${prefix}/${id}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        }
    })
    .then(response => response.json())
    .then(result => {
        myForm.id.value = result.id
        myForm.name.value = result.name
        myForm.phone.value = result.mobilePhone
        myForm.job.value = result.jobTitle
        myForm.birthDate.value = getShortDate(result.birthDate)
    })
    .then(() => modalWindow.hidden = false)
    .catch(error => console.error('Unable to get contacts', error))
}


function getShortDate(dateTime, viewDate) {
    let getCurrentDay = new Date(dateTime);
    let year = getCurrentDay.getFullYear();
    let month = getCurrentDay.getMonth() + 1;
    let day = getCurrentDay.getDate();
    if (viewDate) {
        return checkMonthDay(day) + "/" + checkMonthDay(month) + "/" + year;
    } else {
        return year + "-" + checkMonthDay(month) + "-" + checkMonthDay(day);
    }
}

function checkMonthDay(item) {
    return item >= 10 ? item : "0" + item
}



function editContact(id, contact) {
    fetch(`${prefix}/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify({
            name: contact.name,
            mobilePhone: contact.phone,
            jobTitle: contact.job,
            birthDate: contact.birthDate
        })
    })
    .then(response => {
        if (response.ok !== true) {
            throw new Error("Something went wrong")
        }
    })
    .then(() => getAllUsers())
    .catch(error => console.error('Unable to add contact', error))
}


function addContact(contact) {
    fetch(prefix, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify({
            name: contact.name,
            mobilePhone: contact.phone,
            jobTitle: contact.job,
            birthDate: contact.birthDate
        })
    })
    .then(() => {
        getAllUsers()
    })
    .catch(error => console.error('Unable to add contact', error))
}



function deleteUser(id) {
    fetch(`${prefix}/${id}`, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        }
    })
    .then(() => {
        getAllUsers()
        })
    .catch(error => console.error('Unable to delete contact', error))
}


myForm.addEventListener('focus', event => {
    if (event.target.tagName === 'INPUT') {
        if (event.target.classList.contains('error')) {
            removeInvalid(event.target)
        }
        event.target.classList.add('focus');
    }
   
}, true)


myForm.addEventListener('blur', event => {
    if (event.target.tagName === 'INPUT') {
        event.target.classList.remove('focus');

        if (event.target.value.trim() === "") {
            addInvalid(event.target, 'Field is required!');
        }
    }
}, true)


function addInvalid(field, message) {
    if (field.tagName === 'INPUT') {
        field.classList.add('error');
    }

    let span = document.createElement('span')
    span.classList.add('errorMessage')
    span.innerHTML = message
    field.closest('label').append(span)
}


function removeInvalid(field) {
    let item = field.closest('div').querySelector('.errorMessage')
    if (item !== null) {
        item.remove()
    }
    field.classList.remove('error')
}

function cleaningForm() {
    let fields = myForm.elements
    for (var i = 0; i < fields.length; i++) {
        removeInvalid(fields[i])
    }
}