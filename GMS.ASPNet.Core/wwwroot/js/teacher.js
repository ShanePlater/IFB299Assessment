jQuery(document).ready(($) => {
    $('#full-calendar').fullCalendar({
        navLinks: true,
        editable: true,
        eventLimit: false,
        header: {
            left: '',
            center: 'title',
            right: ''
        },
        events: ''
    });
});
