function afterViewInit() {
    const today =new Date().toISOString().split("T")[0];
    immigrationDate.max = today;
    birthDate.max = today;
    resultMassageContainer.style.display = "none";
}
function register() {
    const registerFrom = {
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
        immigrationDate: immigrationDate.value? immigrationDate.value: null,
        nation: nation.value
       
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
