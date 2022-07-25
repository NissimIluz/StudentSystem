function afterViewInit() {
    const today =new Date().toISOString().split("T")[0];
    immigrationDate.max = today;
    birthDate.max = today;
    resultMassageContainer.style.display = "none";
}
function register() {
    const registerFrom1 = {
        id: id.value,
        school: school.value,
        lastName: lastName.value,
        firstName: firstName.value,
        maleOrFemale: maleOrFemale.value,
        homePhone: homePhone.value,
        mobilePhone: mobilePhone.value,
        email: email.value,
        birthDate: birthDate.value,
        CountryOfBirth: CountryOfBirth.value,
        immigrationDate: immigrationDate.value,
        nation: nation.value
       
    };
    const registerFrom = {
        id: 203318977,
        school: 'בית ספר',
        lastName: 'מפשחה',
        firstName: 'פרטי',
        maleOrFemale: 0,
        homePhone: '08-4561234',
        mobilePhone: '052-4561234',
        email: 'email@sad.co.il',
        birthDate: new Date(),
        CountryOfBirth: 'ישראל',
        immigrationDate: new Date(),
        nation: 'ישראל'
       
    };
    var resultPromise = post('Students/Register',registerFrom);
    resultPromise.then(result => showMassage(result));
    return false;
}

function showMassage(result){
    resultMassageContainer.style.display = "flex";
    if(result===true) {
        resultMassageContainer.classList.add("success");
        resultMassage.textContent  = "הרישום בוצע בהצלחה";
    }
    else {
        resultMassage.textContent  = "שגיאה ברישום";
        resultMassageContainer.classList.add("failed");
    }
    resultMassageContainer.classList.re
    setTimeout(()=> {
        resultMassage.textContent ='';
        resultMassageContainer.style.display = "none";
        resultMassageContainer.classList.remove("failed");
        resultMassageContainer.classList.remove("success");
        }
        , 15000);
}
