 //Boite de dialogue
<Script>
        $("#add-address").click(function() {
          var url = '@Url.Action("Create", "Adresses")';
            $("#dialog-address").load(url, function() {
                $(this).dialog({
                    modal: true,
                    buttons: {
                        "Enregistrer": function () {
                            // Récupérer les données du formulaire
                            var data = $("#address-form").serialize();

                            // Envoyer les données du formulaire au serveur
                            $.post(url, data, function (response) {
                                // Traiter la réponse du serveur
                                // ...
                            });
                        },
                        Annuler: function () {
                            $(this).dialog("close");
                        }
                    }
                });
          });
    });

    $("#edit-address").click(function() {
          var url = '@Url.Action("Edit", "Adresses")';
        $("#dialog-address").load(url, function() {
            $(this).dialog({
                modal: true,
                buttons: {
                    "Enregistrer": function () {
                        // Récupérer les données du formulaire
                        var data = $("#address-form").serialize();

                        // Envoyer les données du formulaire au serveur
                        $.post(url, data, function (response) {
                            // Traiter la réponse du serveur
                            // ...
                        });
                    },
                    Annuler: function () {
                        $(this).dialog("close");
                    }
                }
            });
        });
    });
</Script>