function deleteItem(id) {
    fetch('/Home/Delete/' + id, {
    }).then(res => console.log(res))
        .catch(err => console.log(err))
}
