function btnPassive(itemid,itemurl) {
        Swal.fire({
            title: 'Emin misiniz?',
            text: "Bu veriyi pasif hale getirmek istediğinize emin misiniz?",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Evet Pasif Et!',
            confirmButtonClass: 'btn btn-warning',
            cancelButtonClass: 'btn btn-danger ml-1',
            buttonsStyling: false,
        }).then(function (result) {
            if (result.value) {
                $.ajax({
                    data: { id: itemid },
                    url: itemurl,
                    success: function () {
                        Swal.fire(
                            {
                                type: "success",
                                title: 'İşlem Başarılı!',
                                text: 'Veriniz pasif hale getirildi.',
                                confirmButtonClass: 'btn btn-success',
                            }
                        )
                        window.location.reload();
                    }
                })
               
            }
        })

}


function btnDelete(itemid, itemurl) {
    Swal.fire({
        title: 'Emin misiniz?',
        text: "Bu veriyi silmek istediğinize emin misiniz?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Evet, Sil!',
        confirmButtonClass: 'btn btn-danger',
        cancelButtonClass: 'btn btn-primary ml-1',
        buttonsStyling: false,
    }).then(function (result) {
        if (result.value) {
            $.ajax({
                url: itemurl,
                data: { id: itemid },
                success: function () {
                    Swal.fire(
                        {
                            type: "success",
                            title: 'İşlem Başarılı!',
                            text: 'Veri başarıyla silindi.',
                            confirmButtonClass: 'btn btn-success',
                        }
                    ).then((result) => {
                        if (result.isConfirmed) {
                            window.location.reload();
                        }
                    })
                }
            })

        }
    })

}


